using DataAccess.DTOs.Feedbacks;
using DataAccess.Models;
using FreelancePlatform.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService ifeedbackService)
        {
            _feedbackService = ifeedbackService;
        }

        [HttpPost("leave")]
        public async Task<IActionResult> LeaveFeedback([FromBody] LeaveFeedbackDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Feedback data cannot be null.");
            }

            var result = await _feedbackService.LeaveFeedbackAsync(dto.SenderId, dto.RecipientId, dto.Rating, dto.Comment);

            if (!result)
                return BadRequest("Invalid feedback data.");

            return Ok("Feedback submitted successfully.");
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<FeedbackDto>>> GetFeedbackForUser(int userId)
        {
            var feedback = await _feedbackService.GetFeedbackForUserAsync(userId);

            if (feedback == null || feedback.Count == 0)
                return NotFound("No feedback found for this user.");

            var feedbackDtos = feedback.Select(f => new FeedbackDto
            {
                Id = f.Id,
                SenderId = f.SenderId,
                RecipientId = f.RecipientId,
                Rating = f.Ratings,
                Comment = f.Comment,
                CreatedAt = f.Date,
            }).ToList();

            return Ok(feedbackDtos);
        }


        [HttpGet("user/{userId}/average")]
        public async Task<ActionResult<AverageRatingDto>> GetAverageRating(int userId)
        {
            var avg = await _feedbackService.GetAverageRatingAsync(userId);

            var dto = new AverageRatingDto
            {
                UserId = userId,
                AverageRating = avg
            };

            return Ok(dto);
        }

    }
}
