using BankSystem.Models;

namespace BankSystem.Common
{
    public static class Helper
    {
        public const string InitialBalanceMinimumMessage = "Initial account balance must be at least $100.";
        public const string PanCardOrNameRequiredMessage = "Pan card or name should be present";
        public const string AccountNotFoundMessage = "Account not found";
        public const string AmountShouldBeGreaterThanZeroMessage = "Amount should be greater than 0";
        public const string WithdrawalLimitExceededMessage = "Cannot withdraw more than 90% of your total balance in a single transaction";
        public const string AccountBalanceMinimumMessage = "Account balance cannot be less than $100";
        public const string DepositLimitExceededMessage = "Cannot deposit more than $10,000 in a single transaction";
        public const string ValidUserId = "Please enter a valid User Id";
        public static string GetNextUserId(List<User> users)
        {
            if (users.Any())
            {
                // Find the highest User ID
                var maxUserId = users.Max(x => int.Parse(x.Id.Substring(3)));

                // Increment the numeric part and create the next User ID.
                var nextId = (maxUserId + 1).ToString("D3"); 
                return "SBI" + nextId;
            }
            else
            {
                return "SBI001";
            }
        }

        public static string GetNextAccountID(List<Account> accounts)
        {
            if (accounts.Any())
            {
                // Find the highest account ID among all accounts.
                var maxAccountId = accounts.Max(account => int.Parse(account.Id.Substring(3)));

                // Increment the numeric part and create the next account ID.
                var nextAccountNumber = (maxAccountId + 1).ToString("D3"); // Format as "ACCXXX"
                return "ACC" + nextAccountNumber;
            }
            else
            {
                // If there are no existing accounts, create the first account ID.
                return "ACC001";
            }
        }

    }
}
