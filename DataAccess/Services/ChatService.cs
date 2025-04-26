using DataAccess.Date;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class ChatService : IChatService
    {
        private readonly FreelancePlatformDbContext _context;

        public ChatService(FreelancePlatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<Chat>> GetAllChatsAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<Chat?> GetChatByIdAsync(int id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public async Task CreateChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateChatAsync(Chat chat)
        {
            var existingChat = await _context.Chats.FindAsync(chat.Id);
            if (existingChat == null)
                return false;

            existingChat.Message = chat.Message; // Оновити інші поля, якщо необхідно
            _context.Entry(existingChat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteChatAsync(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
                return false;

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SendMessageAsync(Chat message)
        {
            if (message == null) return false;

            _context.Chats.Add(message);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Chat>> GetChatHistoryAsync(int chatId)
        {
            return await _context.Chats
                .Where(c => c.Id == chatId)
                .ToListAsync();
        }
    }
}

