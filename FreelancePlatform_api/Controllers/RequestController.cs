using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;

namespace FreelancerPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestsController(RequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest([FromBody] Request request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            var result = await _requestService.SubmitRequestAsync(request);
            if (result)
                return CreatedAtAction(nameof(GetRequestsForProjects), new { projectId = request.ProjectId }, request);

            return BadRequest("Request already exists.");
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetRequestsForProjects(int projectId)
        {
            var requests = await _requestService.GetRequestsForProjectsAsync(projectId);
            if (requests == null || requests.Count == 0)
                return NotFound("No requests found for this project.");

            return Ok(requests);
        }
    }
}
