using BankSystem.Common;
using BankSystem.Models;
using System.Security.Principal;

namespace BankSystem.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> usersList;
        public UserService()
        {
            usersList = new List<User>
            {
                // Initialize with mock data
                { new User { Id = "SBI001", Name = "Ankur", PanCard = "ABCD" } },
                { new User { Id = "SBI002", Name = "Rahul", PanCard = "ZXCV" } },
                { new User { Id = "SBI003", Name = "Vikas", PanCard = "ASDF" } },
            };
        }

        public async Task<User> GetUserAsync(string userId)
        {
            try
            {
                return await Task.FromResult(usersList.SingleOrDefault(user => user.Id == userId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                var nextUserId = Helper.GetNextUserId(usersList);
                var newUser = new User
                {
                    Id = nextUserId,
                    Name = user.Name,
                    PanCard = user.PanCard
                };

                usersList.Add(newUser);
                return await Task.FromResult(newUser);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
