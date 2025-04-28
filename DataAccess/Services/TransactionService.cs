using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Date;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Helpers;

namespace DataAccess.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly FreelancePlatformDbContext _context;

        public TransactionService(FreelancePlatformDbContext context)
        {
            _context = context;
        }

        // Оновлений метод з правильним типом ServiceResult<Transaction>
        public async Task<ServiceResult<Transaction>> CreateTransactionAsync(Transaction transaction)
        {
            if (transaction.Amount <= 0)
            {
                return ServiceResult<Transaction>.Failure("Amount must be greater than zero.");
            }

            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Id == transaction.SenderId);
            var receiver = await _context.Users.FirstOrDefaultAsync(u => u.Id == transaction.ReceiverId);

            if (sender == null || receiver == null)
            {
                return ServiceResult<Transaction>.Failure("Sender or receiver not found.");
            }

            _context.Transactions.Add(transaction);
            sender.Balance -= transaction.Amount;
            receiver.Balance += transaction.Amount;

            var result = await _context.SaveChangesAsync() > 0;
            return result ? ServiceResult<Transaction>.SuccessResult(transaction, "Transaction created successfully.") : ServiceResult<Transaction>.Failure("Transaction creation failed.");
        }

        // Оновлений метод з правильним типом ServiceResult<Transaction>
        public async Task<ServiceResult<Transaction>> DepositFundsAsync(int userId, decimal amount)
        {
            if (amount <= 0)
            {
                return ServiceResult<Transaction>.Failure("Amount must be greater than zero.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return ServiceResult<Transaction>.Failure("User not found.");
            }

            user.Balance += amount;

            var transaction = new Transaction
            {
                SenderId = null,
                Amount = amount,
                Date = DateTime.Now,
                Description = $"Deposit of {amount} USD"
            };

            _context.Transactions.Add(transaction);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? ServiceResult<Transaction>.SuccessResult(transaction, "Funds deposited successfully.") : ServiceResult<Transaction>.Failure("Deposit failed.");
        }

        // Оновлений метод з правильним типом ServiceResult<Transaction>
        public async Task<ServiceResult<Transaction>> WithdrawFundsAsync(int userId, decimal amount)
        {
            if (amount <= 0)
            {
                return ServiceResult<Transaction>.Failure("Amount must be greater than zero.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return ServiceResult<Transaction>.Failure("User not found.");
            }

            if (user.Balance < amount)
            {
                return ServiceResult<Transaction>.Failure("Insufficient funds.");
            }

            user.Balance -= amount;

            var transaction = new Transaction
            {
                SenderId = userId,
                ReceiverId = null, // No receiver for withdrawals
                Amount = amount,
                Date = DateTime.Now,
                Description = "Withdrawal"
            };

            _context.Transactions.Add(transaction);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? ServiceResult<Transaction>.SuccessResult(transaction, "Funds withdrawn successfully.") : ServiceResult<Transaction>.Failure("Withdrawal failed.");
        }

        // Метод без змін, так як тут немає змін типу
        public async Task<List<Transaction>> GetTransactionsHistoryAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.SenderId == userId || t.ReceiverId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }
    }
}
