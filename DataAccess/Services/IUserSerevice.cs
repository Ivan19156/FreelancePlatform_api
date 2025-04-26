using DataAccess.Models;
using DataAccess.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IUserService
    {
        Task<ServiceResult<User>> RegisterUserAsync(User user);
        Task<ServiceResult<User>> AuthenticateUserAsync(string email, string password);
        Task<ServiceResult<User>> UpdateUserProfileAsync(User user);
        Task<ServiceResult<User>> AdjustBalanceAsync(int userId, decimal amount);
        Task<List<User>> GetFreelancersAsync();
    }
}

