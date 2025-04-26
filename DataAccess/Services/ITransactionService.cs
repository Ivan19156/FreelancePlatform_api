using DataAccess.Models;
using DataAccess.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface ITransactionService
    {
        Task<ServiceResult<Transaction>> CreateTransactionAsync(Transaction transaction);
        Task<ServiceResult<Transaction>> DepositFundsAsync(int userId, decimal amount);
        Task<ServiceResult<Transaction>> WithdrawFundsAsync(int userId, decimal amount);
        Task<List<Transaction>> GetTransactionsHistoryAsync(int userId);
    }
}

