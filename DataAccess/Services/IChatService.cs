using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IChatService
    {
        Task<List<Chat>> GetAllChatsAsync();
        Task<Chat?> GetChatByIdAsync(int id);
        Task CreateChatAsync(Chat chat);
        Task<bool> UpdateChatAsync(Chat chat);
        Task<bool> DeleteChatAsync(int id);
        Task<bool> SendMessageAsync(Chat message);
        Task<List<Chat>> GetChatHistoryAsync(int chatId);
    }
}

