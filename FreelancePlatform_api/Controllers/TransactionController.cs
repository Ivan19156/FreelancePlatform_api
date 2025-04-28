using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;
using FreelancePlatform.Core.DTOs.Transactions;

namespace FreelancerPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService itransactionService)
        {
            _transactionService = itransactionService;
        }

        // Створення транзакції
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid transaction data.");
            }

            // Мапиш DTO в Entity
            var transaction = new Transaction
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Amount = dto.Amount,
                Type = dto.Type,
                Description = dto.Description,
                Date = DateTime.UtcNow // або DateTime.Now, якщо хочеш локальний час
            };

            var result = await _transactionService.CreateTransactionAsync(transaction);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }


        // Депонування коштів на рахунок користувача
        [HttpPost("deposit")]
        public async Task<IActionResult> DepositFunds([FromBody] DepositFundsDto dto)
        {
            if (dto.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            var result = await _transactionService.DepositFundsAsync(dto.UserId, dto.Amount);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }


        // Виведення коштів з рахунку користувача
        [HttpPost("withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> WithdrawFunds([FromBody] WithdrawFundsDto dto)
        {
            if (dto.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            var result = await _transactionService.WithdrawFundsAsync(dto.UserId, dto.Amount);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }


        // Отримання історії транзакцій для користувача
        [HttpGet("history/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactionsHistory([FromRoute] int userId)
        {
            var transactions = await _transactionService.GetTransactionsHistoryAsync(userId);

            if (transactions == null || !transactions.Any())
            {
                return NotFound("No transactions found for this user.");
            }

            return Ok(transactions);
        }

    }
}

