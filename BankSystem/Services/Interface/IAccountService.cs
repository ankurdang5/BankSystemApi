using BankSystem.Models;

namespace BankSystem.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountAsync(int accountId);
        Task<Account> CreateAccountAsync(string name, decimal balance);
        Task<Account> UpdateAccountAsync(int accountId, string name, decimal balance);
        Task DeleteAccountAsync(int accountId);
        Task<Account> DepositAsync(int accountId, decimal amount);
        Task<Account> WithdrawAsync(int accountId, decimal amount);
    }
}
