using BankSystem.Common;
using BankSystem.Models;
namespace BankSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<Account> accountList;
        public readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            accountList = new List<Account>
            {
                new Account { Id = "ACC001", User = new User { Id = "SBI001", Name = "Ankur", PanCard = "QWERTY" }, Balance = 5000 },
                new Account { Id = "ACC002", User = new User { Id = "SBI001", Name = "Ankur", PanCard = "QWERTY" }, Balance = 150 },
                new Account { Id = "ACC003", User = new User { Id = "SBI002", Name = "Rohan", PanCard = "ZXCVBN" }, Balance = 10000 }
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
                var accountdetail = await Task.FromResult(accountList.SingleOrDefault(x => x.Id == accountId));
                if (accountdetail == null)
                {
                    throw new InvalidOperationException(Helper.AccountNotFoundMessage);
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
                    throw new InvalidOperationException(Helper.InitialBalanceMinimumMessage);
                }
                User user = new User();
                if (string.IsNullOrEmpty(userid))
                {
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(panCard))
                    {
                        throw new InvalidOperationException(Helper.PanCardOrNameRequiredMessage);
                    }
                    user.Name = userName;
                    user.PanCard = panCard;
                    user = await _userService.CreateUserAsync(user);
                }
                else
                {
                    user = await _userService.GetUserAsync(userid);
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
                    throw new InvalidOperationException(Helper.AmountShouldBeGreaterThanZeroMessage);
                }

                if (amount > 10000)
                {
                    throw new InvalidOperationException(Helper.DepositLimitExceededMessage);
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
                    throw new InvalidOperationException(Helper.AmountShouldBeGreaterThanZeroMessage);
                }
                var account = await GetAccountAsync(accountId);


                if (amount > account.Balance * 0.9m)
                {
                    throw new InvalidOperationException(Helper.WithdrawalLimitExceededMessage);
                }
                if (account.Balance - amount < 100)
                {
                    throw new InvalidOperationException(Helper.AccountBalanceMinimumMessage);
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
