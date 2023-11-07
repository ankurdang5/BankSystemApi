using BankSystem.Models;

namespace BankSystem.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountAsync(string accountId);
        Task<Account> CreateAccountAsync(string userid, string userName, string panCard, decimal balance);
        Task DeleteAccountAsync(string accountId);
        Task<Account> DepositAsync(string accountId, decimal amount);
        Task<Account> WithdrawAsync(string accountId, decimal amount);
    }
}
