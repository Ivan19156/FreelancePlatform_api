using DataAccess.Models;
using FreelancePlatform.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("leave")]
        public async Task<IActionResult> LeaveFeedback(int senderId, int recipientId, int rating, string comment)
        {
            var result = await _feedbackService.LeaveFeedbackAsync(senderId, recipientId, rating, comment);
            if (!result)
                return BadRequest("Invalid feedback data.");
            return Ok("Feedback submitted successfully.");
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Feedback>>> GetFeedbackForUser(int userId)
        {
            var feedback = await _feedbackService.GetFeedbackForUserAsync(userId);
            return Ok(feedback);
        }

        [HttpGet("user/{userId}/average")]
        public async Task<ActionResult<double>> GetAverageRating(int userId)
        {
            var avg = await _feedbackService.GetAverageRatingAsync(userId);
            return Ok(avg);
        }
    }
}
