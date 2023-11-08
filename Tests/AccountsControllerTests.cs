using BankSystem.Controllers;
using BankSystem.Models;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystem.Tests
{
    [TestClass]
    public class AccountsControllerTests
    {
        private AccountsController _controller;
        private Mock<IAccountService> _accountServiceMock;
        private Mock<ILogger<AccountsController>> _loggerMock;

        [TestInitialize]
        public void Initialize()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _loggerMock = new Mock<ILogger<AccountsController>>();
            _controller = new AccountsController(_accountServiceMock.Object, _loggerMock.Object);
        }
        [TestMethod]
        public async Task GetAccounts_ShouldReturnListOfAccounts()
        {
            var accounts = new List<Account>
    {
        new Account { Id = "ACC001", User = new User { Id = "SBI001" }, Balance = 1000 },
        new Account { Id = "ACC002", User = new User { Id = "SBI002" }, Balance = 2000 }
    };

            _accountServiceMock.Setup(x => x.GetAllAccountsAsync()).ReturnsAsync(accounts);

            var result = await _controller.Account();
            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedAccounts = okResult.Value as IEnumerable<Account>;
            Assert.IsNotNull(returnedAccounts);
            Assert.AreEqual(2, returnedAccounts.Count());
        }

        [TestMethod]
        public async Task GetAccount_WithValidAccountId_ShouldReturnAccount()
        {
            // Arrange
            var account = new Account { Id = "ACC001", User = new User { Id = "SBI001" }, Balance = 1000 };
            _accountServiceMock.Setup(x => x.GetAccountAsync("ACC001")).ReturnsAsync(account);

            // Act
            var result = await _controller.Account("ACC001");
            // Assert
            Assert.IsNotNull(result.Value);
            var returnedAccount = result.Value as Account;
            Assert.IsNotNull(returnedAccount);
            Assert.AreEqual("ACC001", returnedAccount.Id);
        }

    }
}