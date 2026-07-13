using Microsoft.EntityFrameworkCore;
using Spifel.Domain.Contracts;
using Spifel.Domain.Models.User;
using Spifel.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Spifel.Infra.Data.Repositories
{
    public class UserRepository(SpifelDbContext context) : IUserRepository
    {
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.id == userId);
        }
        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await context.Users.ToListAsync();
        }
        public async Task CreateAsync(User user)
        {
            await context.Users.AddAsync(user);
        }
        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
        }
        public async Task DeleteAsync(User user)
        {
            user.IsDeleted = true;
            user.IsActive = false;
            user.CreateDate = DateTime.Now;
            await UpdateAsync(user);
        }
        public async Task DeleteAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                await DeleteAsync(user);
            }
        }
        public async Task<bool> IsEmailExist(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> IsUserNameExist(string userName)
        {
            return await context.Users.AnyAsync(u => u.UserName == userName);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByActiveCodeAsync(string activeCode)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.EmailActiveCode == activeCode);
        }

        public async Task<User?> GetUserByEmailOrUserNameAsync(string emailOrUserName)
        {
            return await context.Users.SingleOrDefaultAsync(u =>
            u.UserName == emailOrUserName ||
            u.Email == emailOrUserName);
        }
    }
}
