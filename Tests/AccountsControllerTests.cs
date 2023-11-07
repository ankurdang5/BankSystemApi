//using BankSystem.Controllers;
//using BankSystem.Models;
//using BankSystem.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace Tests
//{
//    [TestClass]
//    public class AccountsControllerTests
//    {
//        [TestMethod]
//        public async Task GetAccounts_ReturnsListOfAccounts()
//        {
//            var accountService = new AccountService();
//            var logger = Mock.Of<ILogger>();
//            var controller = new AccountsController(accountService,logger);
//            var result = await controller.Account();
//            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
//            var okResult = (OkObjectResult)result.Result;
//            var accounts = (IEnumerable<Account>)okResult.Value;
//            Assert.IsNotNull(accounts);
//        }

//        [TestMethod]
//        public async Task GetAccount_ExistingAccountId_ReturnsAccount()
//        {
//            var accountId = 1;
//            var accountService = new AccountService();
//            var logger = Mock.Of<ILogger>();
//            var controller = new AccountsController(accountService, logger);
//            var result = await controller.Account(accountId);
//            var account = result.Value;
//            Assert.IsInstanceOfType(account, typeof(Account));
//        }

//        [TestMethod]
//        public async Task CreateAccount_ValidData_ReturnsCreatedAccount()
//        {
//            var request = new CreateAccountRequest
//            {
//                Name = "TestAccount",
//                Balance = 1000.0m,
//            };
//            var accountService = new AccountService();
//            var logger = Mock.Of<ILogger<AccountsController>>();
//            var controller = new AccountsController(accountService, logger);
//            var result = await controller.Account(request);
//            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
//            var createdAtResult = (CreatedAtActionResult)result.Result;
//            Assert.AreEqual("GetAccount", createdAtResult.ActionName);
//            var account = (Account)createdAtResult.Value;
//            Assert.IsNotNull(account);
//        }

//        [TestMethod]
//        public async Task UpdateAccount_ExistingAccountId_ReturnsAccount()
//        {
//            var accountId = 2;
//            var request = new UpdateAccountRequest
//            {
//                Name = "TestAccount",
//                Balance = 6969.0m,
//            };
//            var accountService = new AccountService();
//            var logger = Mock.Of<ILogger<AccountsController>>();
//            var controller = new AccountsController(accountService, logger);
//            var result = await controller.Account(accountId, request);
//            var account = result.Value;
//            Assert.IsInstanceOfType(account, typeof(Account));
//        }

//        [TestMethod]
//        public async Task DeleteAccount_ExistingAccountId()
//        {
//            var accountId = 3;
//            var accountService = new AccountService();
//            var logger = Mock.Of<ILogger<AccountsController>>();
//            var controller = new AccountsController(accountService, logger);
//            var result = await controller.Delete(accountId);
//            Assert.IsInstanceOfType(result, typeof(NoContentResult));
//        }

//        [TestMethod]
//        public async Task DepositAccount_ExistingAccountId_ReturnsAccount()
//        {
//            var accountId = 1;
//            var request = new DepositRequest
//            {
//                Amount = 2000.0m,
//            };
//            var accountService = new AccountService();
//            var logger = Mock.Of<ILogger<AccountsController>>();
//            var controller = new AccountsController(accountService, logger);
//            var result = await controller.Deposit(accountId, request);
//            var account = result.Value;
//            Assert.IsInstanceOfType(account, typeof(Account));
//        }

//        [TestMethod]
//        public async Task WithdrawAccount_ExistingAccountId_ReturnsAccount()
//        {
//            var accountId = 3;
//            var request = new WithdrawRequest
//            {
//                Amount = 2000.0m,
//            };
//            var accountService = new AccountService();
//            var logger = Mock.Of<ILogger<AccountsController>>();
//            var controller = new AccountsController(accountService, logger);
//            var result = await controller.Withdraw(accountId, request);
//            var account = result.Value;
//            Assert.IsInstanceOfType(account, typeof(Account));
//        }
//    }
//}
