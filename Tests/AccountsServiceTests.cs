using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using BankSystem.Models;
using BankSystem.Services;
using Moq;

namespace BankSystem.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        private AccountService _accountService;
        private Mock<IUserService> _userServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _userServiceMock = new Mock<IUserService>();
            _accountService = new AccountService(_userServiceMock.Object);
        }
        [TestMethod]
        public async Task CreateAccountAsync_WithValidInput_ShouldCreateAccount()
        {
            var newUser = new User { Name = "Raj", PanCard = "ABCD1234" };
            _userServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(newUser);

            var account = await _accountService.CreateAccountAsync(null, "John", "ABCD1234", 1000);

            // Assert
            Assert.IsNotNull(account);
            Assert.AreEqual("ACC004", account.Id); // Assumes Helper.GetNextAccountID(accountList) returns "ACC004"
            Assert.AreEqual(newUser, account.User);
            Assert.AreEqual(1000, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CreateAccountAsync_WithInvalidBalance_ShouldThrowException()
        {
            await _accountService.CreateAccountAsync(null, "John", "ABCD1234", 50); // Balance < 100 should throw an exception
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CreateAccountAsync_WithMissingUserNameAndPanCard_ShouldThrowException()
        {
            await _accountService.CreateAccountAsync(null, null, null, 1000); // Missing UserName and PanCard should throw an exception
        }

        [TestMethod]
        public async Task DepositAsync_WithValidAmount_ShouldIncreaseBalance()
        {
            // Arrange
            var result = await _accountService.DepositAsync("ACC001", 500);

            Assert.IsNotNull(result);
            Assert.AreEqual(5500, result.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task DepositAsync_WithZeroAmount_ShouldThrowException()
        {
            await _accountService.DepositAsync("ACC001", 0); // Zero amount should throw an exception
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task DepositAsync_WithAmountGreaterThan10000_ShouldThrowException()
        {
            await _accountService.DepositAsync("ACC001", 11000); // Amount > 10000 should throw an exception
        }

        [TestMethod]
        public async Task WithdrawAsync_WithValidAmount_ShouldDecreaseBalance()
        {
            var result = await _accountService.WithdrawAsync("ACC001", 500);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4500, result.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WithdrawAsync_WithAmountGreaterThan90PercentOfBalance_ShouldThrowException()
        {
            await _accountService.WithdrawAsync("ACC001", 4800); // Amount > 90% of balance should throw an exception
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WithdrawAsync_WithBalanceLessThan100_ShouldThrowException()
        {
            await _accountService.WithdrawAsync("ACC002", 100); // Balance < 100 should throw an exception
        }


        [TestMethod]
        public async Task GetAllAccountsAsync_ShouldReturnAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();

            // Assert
            Assert.IsNotNull(accounts);
            Assert.AreEqual(3, accounts.Count()); // Assuming there are 3 accounts in the initial list
        }


        [TestMethod]
        public async Task GetAccountAsync_WithValidAccountId_ShouldReturnAccount()
        {
            var account = await _accountService.GetAccountAsync("ACC001");

            // Assert
            Assert.IsNotNull(account);
            Assert.AreEqual("ACC001", account.Id);
        }
    }
}
