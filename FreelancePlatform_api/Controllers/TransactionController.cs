using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;

namespace FreelancerPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Створення транзакції
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Invalid transaction data.");
            }

            var result = await _transactionService.CreateTransactionAsync(transaction);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        // Депонування коштів на рахунок користувача
        [HttpPost("deposit")]
        public async Task<IActionResult> DepositFunds([FromQuery] int userId, [FromQuery] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            var result = await _transactionService.DepositFundsAsync(userId, amount);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        // Виведення коштів з рахунку користувача
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawFunds([FromQuery] int userId, [FromQuery] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            var result = await _transactionService.WithdrawFundsAsync(userId, amount);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        // Отримання історії транзакцій для користувача
        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetTransactionsHistory(int userId)
        {
            var transactions = await _transactionService.GetTransactionsHistoryAsync(userId);
            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("No transactions found for this user.");
            }

            return Ok(transactions);
        }
    }
}

