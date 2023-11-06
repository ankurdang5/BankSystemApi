using BankSystem.Models;
namespace BankSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDictionary<int, Account> _accounts;

        public AccountService()
        {
            _accounts = new Dictionary<int, Account>
            {
                // Initialize with mock data
                { 1, new Account { Id = 1, Name = "Ankur Account1", Balance = 5000 } },
                { 2, new Account { Id = 2, Name = "Ankur Account2", Balance = 3000 } },
                { 3, new Account { Id = 3, Name = "Jitu Account1", Balance = 10000 } }
            };
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            try
            {
                return await Task.FromResult(_accounts.Values);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Account> GetAccountAsync(int accountId)
        {
            try
            {
                if (!_accounts.ContainsKey(accountId))
                {
                    throw new InvalidOperationException("Account not found.");
                }
                return await Task.FromResult(_accounts[accountId]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> CreateAccountAsync(string name, decimal balance)
        {
            try
            {
                if (balance < 100)
                {
                    throw new InvalidOperationException("Initial account balance must be at least $100.");
                }
                var account = new Account
                {
                    Id = _accounts.Keys.Max() + 1,
                    Name = name,
                    Balance = balance
                };
                _accounts.Add(account.Id, account);
                return await Task.FromResult(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> UpdateAccountAsync(int accountId, string name, decimal balance)
        {
            try
            {
                if (!_accounts.ContainsKey(accountId))
                {
                    throw new InvalidOperationException("Account not found.");
                }
                var account = _accounts[accountId];
                account.Name = name;
                account.Balance = balance;
                _accounts[accountId] = account;
                return await Task.FromResult(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            try
            {
                if (!_accounts.ContainsKey(accountId))
                {
                    return;
                }

                _accounts.Remove(accountId);

                await Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> DepositAsync(int accountId, decimal amount)
        {
            try
            {
                if (!_accounts.ContainsKey(accountId))
                {
                    throw new InvalidOperationException("Account not found.");
                }

                if (amount > 10000)
                {
                    throw new InvalidOperationException("Cannot deposit more than $10,000 in a single transaction.");
                }

                var account = _accounts[accountId];
                account.Balance += amount;

                _accounts[accountId] = account;

                return await Task.FromResult(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> WithdrawAsync(int accountId, decimal amount)
        {
            try
            {
                if (!_accounts.ContainsKey(accountId))
                {
                    throw new InvalidOperationException("Account not found.");
                }
                var account = _accounts[accountId];

                if (amount > account.Balance * 0.9m)
                {
                    throw new InvalidOperationException("Cannot withdraw more than 90% of your total balance in a single transaction.");
                }
                if (account.Balance - amount < 100)
                {
                    throw new InvalidOperationException("Account balance cannot be less than $100.");
                }
                account.Balance -= amount;
                _accounts[accountId] = account;
                return await Task.FromResult(account);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
