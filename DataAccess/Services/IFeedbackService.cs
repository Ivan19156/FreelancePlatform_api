using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelancePlatform.Core.Services
{
    public interface IFeedbackService
    {
        Task<bool> LeaveFeedbackAsync(int senderId, int recipientId, int rating, string comment);
        Task<List<Feedback>> GetFeedbackForUserAsync(int userId);
        Task<double> GetAverageRatingAsync(int userId);
    }
}
