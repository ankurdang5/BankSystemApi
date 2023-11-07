using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using BankSystem.Models;
using BankSystem.Services;

namespace BankSystem.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        private AccountService accountService;

        [TestInitialize]
        public void Initialize()
        {
            accountService = new AccountService();
        }

        [TestMethod]
        public async Task GetAllAccountsAsync_ReturnsAllAccounts()
        {
            // Arrange: No specific arrangement needed

            // Act
            var accounts = await accountService.GetAllAccountsAsync();

            // Assert
            Assert.AreEqual(3, accounts.Count());
        }

        [TestMethod]
        public async Task GetAccountAsync_ExistingAccount_ReturnsAccount()
        {
            // Arrange
            var accountId = accountService.GetAllAccountsAsync().Result.First().Id;

            // Act
            var account = await accountService.GetAccountAsync(accountId);

            // Assert
            Assert.IsNotNull(account);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetAccountAsync_NonExistingAccount_ThrowsException()
        {
            // Arrange: Use an invalid accountId
            var accountId = Guid.NewGuid().ToString();

            // Act
            var account = await accountService.GetAccountAsync(accountId);

            // Assert: An exception should be thrown
        }

        // Add more test methods for other service methods (CreateAccountAsync, DepositAsync, WithdrawAsync, etc.)
    }
}
