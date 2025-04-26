using DataAccess.Date;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelancePlatform.Core.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly FreelancePlatformDbContext _context;

        public FeedbackService(FreelancePlatformDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LeaveFeedbackAsync(int senderId, int recipientId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
                return false;

            var sender = await _context.Users.FindAsync(senderId);
            var recipient = await _context.Users.FindAsync(recipientId);

            if (sender == null || recipient == null)
                return false;

            var feedback = new Feedback
            {
                SenderId = senderId,
                RecipientId = recipientId,
                Ratings = rating,
                Comment = comment,
                Date = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Feedback>> GetFeedbackForUserAsync(int userId)
        {
            return await _context.Feedbacks
                .Where(f => f.RecipientId == userId)
                .OrderByDescending(f => f.Date)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(int userId)
        {
            var ratings = await _context.Feedbacks
                .Where(f => f.RecipientId == userId)
                .Select(f => f.Ratings)
                .ToListAsync();

            return ratings.Any() ? ratings.Average() : 0;
        }
    }
}
