using BankSystem.Common;
using BankSystem.Models;
namespace BankSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<Account> accountList;
        public readonly IUserService _userService;

        static AccountService()
        {

        }

        public AccountService(IUserService userService)
        {
            accountList = new List<Account>
            {
                new Account { Id = "ACC001", User = new User { Id = "SBI001" }, Balance = 5000 },
                new Account { Id = "ACC002", User = new User { Id = "SBI001" }, Balance = 150 },
                new Account { Id = "ACC003", User = new User { Id = "SBI002" }, Balance = 10000 }
            };
            _userService = userService;
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            try
            {
                return await Task.FromResult(accountList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Account> GetAccountAsync(string accountId)
        {
            try
            {
                var accountdetail = await Task.FromResult(accountList.FirstOrDefault(x => x.Id == accountId));
                if (accountdetail == null)
                {
                    throw new InvalidOperationException("Account not found.");
                }
                return accountdetail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> CreateAccountAsync(string userid, string userName, string panCard, decimal balance)
        {
            try
            {
                if (balance < 100)
                {
                    throw new InvalidOperationException("Initial account balance must be at least $100.");
                }
                User user = new User();
                if (string.IsNullOrEmpty(userid))
                {
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(panCard))
                    {
                        throw new InvalidOperationException("Pan card or name should be present");
                    }
                    user.Name = userName;
                    user.PanCard = panCard;
                    user = await _userService.CreateUserAsync(user);
                }
                var newAccountId = Helper.GetNextAccountID(accountList);
                var account = new Account
                {
                    Id = newAccountId,
                    User = user,
                    Balance = balance
                };

                accountList.Add(account);
                return await Task.FromResult(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAccountAsync(string accountId)
        {
            try
            {
                var accountToDelete = await Task.FromResult(accountList.FirstOrDefault(account => account.Id == accountId));
                if (accountToDelete != null)
                {
                    accountList.Remove(accountToDelete);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> DepositAsync(string accountId, decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new InvalidOperationException("amount should be greater than 0");
                }

                if (amount > 10000)
                {
                    throw new InvalidOperationException("Cannot deposit more than $10,000 in a single transaction.");
                }

                var account = await GetAccountAsync(accountId);
                account.Balance += amount;

                return await Task.FromResult(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> WithdrawAsync(string accountId, decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new InvalidOperationException("Amount should be greater than 0.");
                }
                var account = await GetAccountAsync(accountId);

                if (account.Balance - amount < 100)
                {
                    throw new InvalidOperationException("Account balance cannot be less than $100.");
                }
                if (amount > account.Balance * 0.9m)
                {
                    throw new InvalidOperationException("Cannot withdraw more than 90% of your total balance in a single transaction.");
                }
                account.Balance -= amount;
                return await Task.FromResult(account);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
