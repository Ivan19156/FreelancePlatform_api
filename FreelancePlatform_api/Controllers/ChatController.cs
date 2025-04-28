using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.DTOs.Chats;

namespace FreelancePlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService ichatService)
        {
            _chatService = ichatService;
        }

        // Отримати список чатів
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetChats()
        {
            var chats = await _chatService.GetAllChatsAsync();

            var chatDtos = chats.Select(c => new ChatDto
            {
                Id = c.Id,
                CustomerId = c.RecipientId,
                FreelancerId = c.SenderId
            }).ToList();

            return Ok(chatDtos);
        }


        // Отримати чат за ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChatById(int id)
        {
            var chat = await _chatService.GetChatByIdAsync(id);
            if (chat == null)
                return NotFound();
            return Ok(chat);
        }

        // Створити новий чат
        [HttpPost]
        public async Task<ActionResult> CreateChat([FromBody] CreateChatDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Chat data cannot be null.");
            }

            var chat = new Chat
            {

                RecipientId = dto.CustomerId,
                SenderId = dto.FreelancerId,
                Message = dto.Message,
            };

            await _chatService.CreateChatAsync(chat);

            return CreatedAtAction(nameof(GetChatById), new { id = chat.Id }, new
            {
                chat.Id,
                
                chat.RecipientId,
                chat.SenderId,
                chat.Message,
            });
        }


        // Оновити чат
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChat(int id, [FromBody] UpdateChatDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Chat data cannot be null.");
            }

            var chat = new Chat
            {
                Id = id,
                
                RecipientId = dto.CustomerId,
                SenderId = dto.FreelancerId,
                Message = dto.Message,
            };

            var updated = await _chatService.UpdateChatAsync(chat);
            if (!updated)
                return NotFound("Chat not found.");

            return NoContent();
        }


        // Видалити чат
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(int id)
        {
            var deleted = await _chatService.DeleteChatAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}


