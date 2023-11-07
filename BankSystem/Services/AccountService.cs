using BankSystem.Models;
namespace BankSystem.Services
{
    public class AccountService : IAccountService
    {
        private static readonly List<Account> _accounts;
        public readonly IUserService _userService;

        static AccountService()
        {
            _accounts = new List<Account>
                {
                    new Account { Id = Guid.NewGuid().ToString(), User = new User { Id = "SBI001" }, Balance = 5000 },
                    new Account { Id = Guid.NewGuid().ToString(), User = new User { Id = "SBI001" }, Balance = 3000 },
                    new Account { Id = Guid.NewGuid().ToString(), User = new User { Id = "SBI002" }, Balance = 10000 }
                };
        }

        public AccountService(IUserService userService)
        {
            _userService  = userService;
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            try
            {
                return await Task.FromResult(_accounts);
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
                var accountdetail = await Task.FromResult(_accounts.FirstOrDefault(x => x.Id == accountId));
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
                User user= new User();
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
                var newAccountId = Guid.NewGuid().ToString();
                var account = new Account
                {
                    Id = newAccountId,
                    User = user,
                    Balance = balance
                };

                _accounts.Add(account);
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
                var accountToDelete = await Task.FromResult(_accounts.FirstOrDefault(account => account.Id == accountId));
                if (accountToDelete != null)
                {
                    _accounts.Remove(accountToDelete);
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

                if (amount > account.Balance * 0.9m)
                {
                    throw new InvalidOperationException("Cannot withdraw more than 90% of your total balance in a single transaction.");
                }

                if (account.Balance - amount < 100)
                {
                    throw new InvalidOperationException("Account balance cannot be less than $100.");
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
