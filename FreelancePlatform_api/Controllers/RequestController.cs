using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.DTOs.Requests;

namespace FreelancerPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService irequestService)
        {
            _requestService = irequestService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest([FromBody] SubmitRequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Request cannot be null.");
            }

            var request = new Request
            {
                FreelancerId = dto.FreelancerId,
                ProjectId = dto.ProjectId,
                OfferedPrice = dto.OfferedPrice,
                StartDate = dto.StartDate
            };

            var result = await _requestService.SubmitRequestAsync(request);

            if (result)
            {
                return CreatedAtAction(nameof(GetRequestsForProjects), new { projectId = request.ProjectId }, request);
            }

            return BadRequest("Request already exists.");
        }




        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetRequestsForProjects(int projectId)
        {
            var requests = await _requestService.GetRequestsForProjectsAsync(projectId);
            if (requests == null || requests.Count == 0)
                return NotFound("No requests found for this project.");

            var requestDtos = requests.Select(r => new RequestDto
            {
                Id = r.Id,
                FreelancerId = r.FreelancerId,
                ProjectId = r.ProjectId,
                OfferedPrice = r.OfferedPrice,
                StartDate = r.StartDate
            }).ToList();

            return Ok(requestDtos);
        }

    }
}
