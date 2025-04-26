using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;

namespace FreelancerPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest("Project cannot be null.");
            }
            var result = await _projectService.CreateProjectAsync(project);
            if (result)
                return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
            return BadRequest("Error creating project.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return BadRequest("Project ID mismatch.");
            }

            var result = await _projectService.UpdateProjectAsync(project);
            if (result)
                return Ok(project);
            return NotFound("Project not found or update not allowed.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (result)
                return NoContent();
            return NotFound("Project not found.");
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetProjectsByCustomer(int customerId)
        {
            var projects = await _projectService.GetProjectsByCustomerAsync(customerId);
            if (projects == null || projects.Count == 0)
                return NotFound("No projects found for this customer.");
            return Ok(projects);
        }

        [HttpPost("{projectId}/assign/{freelancerId}")]
        public async Task<IActionResult> AssignFreelancer(int projectId, int freelancerId)
        {
            var result = await _projectService.AssignFreelancerAsync(projectId, freelancerId);
            if (result)
                return Ok("Freelancer assigned successfully.");
            return BadRequest("Error assigning freelancer.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectsByCustomerAsync(id);  // You can change this to a single Project method if needed
            if (project == null)
                return NotFound("Project not found.");
            return Ok(project);
        }
    }
}
