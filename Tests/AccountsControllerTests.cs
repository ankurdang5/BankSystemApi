using BankSystem.Controllers;
using BankSystem.Models;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    [TestClass]
    public class AccountsControllerTests
    {
        [TestMethod]
        public async Task GetAccounts_ReturnsListOfAccounts()
        {
            var accountService = new AccountService(); 
            var controller = new AccountsController(accountService);
            var result = await controller.GetAccounts();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result.Result;
            var accounts = (IEnumerable<Account>)okResult.Value;
            Assert.IsNotNull(accounts);
        }

        [TestMethod]
        public async Task GetAccount_ExistingAccountId_ReturnsAccount()
        {
            var accountId = 1; 
            var accountService = new AccountService();
            var controller = new AccountsController(accountService);
            var result = await controller.GetAccount(accountId);
            var account = result.Value;
            Assert.IsInstanceOfType(account,typeof(Account));
        }

        [TestMethod]
        public async Task CreateAccount_ValidData_ReturnsCreatedAccount()
        {
            var request = new CreateAccountRequest
            {
                Name = "TestAccount",
                Balance = 1000.0m,
            };
            var accountService = new AccountService(); 
            var controller = new AccountsController(accountService);
            var result = await controller.CreateAccount(request);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var createdAtResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual("GetAccount", createdAtResult.ActionName);
            var account = (Account)createdAtResult.Value;
            Assert.IsNotNull(account);
        }
    }
}
