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
    public class UserServiceTests
    {
        [TestMethod]
        public async Task GetUserAsync_WithValidUserId_ShouldReturnUser()
        {
            var userService = new UserService();
            var userId = "SBI001";

            var user = await userService.GetUserAsync(userId);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.Id);
        }

        [TestMethod]
        public async Task GetUserAsync_WithInvalidUserId_ShouldReturnNull()
        {
            var userService = new UserService();
            var userId = "NonExistentUserId";

            var user = await userService.GetUserAsync(userId);

            // Assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task CreateUserAsync_WithValidUser_ShouldCreateUser()
        {
            var userService = new UserService();
            var newUser = new User { Name = "John", PanCard = "EFGH" };

            var createdUser = await userService.CreateUserAsync(newUser);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.AreEqual(newUser.Name, createdUser.Name);
            Assert.AreEqual(newUser.PanCard, createdUser.PanCard);
        }
    }
}
