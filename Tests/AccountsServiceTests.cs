using BankSystem.Models;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    [TestClass]
    public class AccountsServiceTests
    {
        [TestMethod]
        public async Task GetAccounts_ReturnsListOfAccounts()
        {
            var accountService = new AccountService(); 
            var result = await accountService.GetAllAccountsAsync();
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Account>));
            var okResult = new OkObjectResult(result);
            var accounts = (IEnumerable<Account>)okResult.Value;
            Assert.IsNotNull(accounts);
        }

        [TestMethod]
        public async Task GetAccount_ExistingAccountId_ReturnsAccount()
        {
            var accountId = 1; 
            var accountService = new AccountService();
            var result = await accountService.GetAccountAsync(accountId);
            var account = result;
            Assert.IsInstanceOfType(account,typeof(Account));
        }

        [TestMethod]
        public async Task CreateAccount_ValidData_ReturnsCreatedAccount()
        {
            var request = new CreateAccountRequest
            {
                Name = "ServiceTestAccount",
                Balance = 2000.0m,
            };
            var accountService = new AccountService(); 
            var result = await accountService.CreateAccountAsync(request.Name,request.Balance);
            Assert.IsInstanceOfType(result, typeof(Account));
            var account = result;
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public async Task UpdateAccount_ExistingAccountId_ReturnsAccount()
        {
            var accountId = 2;
            var request = new UpdateAccountRequest
            {
                Name = "TestAccount",
                Balance = 6969.0m,
            };
            var accountService = new AccountService();
            var result = await accountService.UpdateAccountAsync(accountId,request.Name,request.Balance);
            var account = result;
            Assert.IsInstanceOfType(account, typeof(Account));
        }

        [TestMethod]
        public async Task DeleteAccount_ExistingAccountId()
        {
            var accountId = 3;
            var accountService = new AccountService();
            await accountService.DeleteAccountAsync(accountId);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task DepositAccount_ExistingAccountId_ReturnsAccount()
        {
            var accountId = 1;
            var request = new DepositRequest
            {
                Amount = 2000.0m,
            };
            var accountService = new AccountService();
            var result = await accountService.DepositAsync(accountId, request.Amount);
            Assert.IsInstanceOfType(result, typeof(Account));
        }

        [TestMethod]
        public async Task WithdrawAccount_ExistingAccountId_ReturnsAccount()
        {
            var accountId = 3;
            var request = new WithdrawRequest
            {
                Amount = 2000.0m,
            };
            var accountService = new AccountService();
            var result = await accountService.WithdrawAsync(accountId, request.Amount);
            Assert.IsInstanceOfType(result, typeof(Account));
        }
    }
}
