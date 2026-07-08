using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userId);
        Task DeleteAsync(User user);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsUserNameExist(string userName);
        Task<User?> GetUserByActiveCodeAsync(string activeCode);
        Task<User?> GetUserByEmailOrUserName(string emailOrUserName);
        Task SaveAsync();
    }
}
