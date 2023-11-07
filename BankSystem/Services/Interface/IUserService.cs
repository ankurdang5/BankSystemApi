using BankSystem.Models;

namespace BankSystem.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);
        Task<User> CreateUserAsync(User user);
    }
}
