using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using DataAccess.Date;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Helpers;

namespace DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly FreelancePlatformDbContext _context;

        public UserService(FreelancePlatformDbContext context)
        {
            _context = context;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private async Task<bool> UserExistsAsync(User user)
        {
            return await _context.Users.AnyAsync(u => u.Email == user.Email);
        }

        // Оновлений метод з правильним типом ServiceResult<User>
        public async Task<ServiceResult<User>> RegisterUserAsync(User user)
        {
            if (!await UserExistsAsync(user))
            {
                user.Password = HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return ServiceResult<User>.SuccessResult(user, "User registered successfully.");
            }
            return ServiceResult<User>.Failure("User already exists.");
        }

        // Оновлений метод з правильним типом ServiceResult<User>
        public async Task<ServiceResult<User>> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return ServiceResult<User>.Failure("User not found.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return isPasswordValid
                ? ServiceResult<User>.SuccessResult(user, "Authentication successful.")
                : ServiceResult<User>.Failure("Invalid password.");
        }

        // Оновлений метод з правильним типом ServiceResult<User>
        public async Task<ServiceResult<User>> UpdateUserProfileAsync(User user)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null)
                return ServiceResult<User>.Failure("User not found.");

            if (!string.IsNullOrEmpty(user.Email)) existingUser.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Name)) existingUser.Name = user.Name;
            if (!string.IsNullOrEmpty(user.Password)) existingUser.Password = HashPassword(user.Password);

            await _context.SaveChangesAsync();
            return ServiceResult<User>.SuccessResult(existingUser, "User profile updated.");
        }

        // Оновлений метод з правильним типом ServiceResult<User>
        public async Task<ServiceResult<User>> AdjustBalanceAsync(int userId, decimal amount)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return ServiceResult<User>.Failure("User not found.");

            user.Balance += amount;
            await _context.SaveChangesAsync();
            return ServiceResult<User>.SuccessResult(user, "Balance updated.");
        }

        // Метод без змін
        public async Task<List<User>> GetFreelancersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
