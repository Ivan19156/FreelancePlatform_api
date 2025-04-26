using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;

namespace FreelancePlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        // Отримати список чатів
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        {
            var chats = await _chatService.GetAllChatsAsync();
            return Ok(chats);
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
        public async Task<ActionResult> CreateChat(Chat chat)
        {
            await _chatService.CreateChatAsync(chat);
            return CreatedAtAction(nameof(GetChatById), new { id = chat.Id }, chat);
        }

        // Оновити чат
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChat(int id, Chat chat)
        {
            if (id != chat.Id)
                return BadRequest();

            var updated = await _chatService.UpdateChatAsync(chat);
            if (!updated)
                return NotFound();

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


