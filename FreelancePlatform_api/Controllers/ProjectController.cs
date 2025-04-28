using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.DTOs.Projects;

namespace FreelancerPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService iprojectService)
        {
            _projectService = iprojectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data cannot be null.");
            }

            // Перетворення DTO на модель
            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                Budget = projectDto.Budget,
                Status = projectDto.Status,
                Category = projectDto.Category,
                CustomerId = projectDto.CustomerId,
                ExecutorId = projectDto.ExecutorId
            };

            // Викликаємо сервіс для створення проекту
            var result = await _projectService.CreateProjectAsync(project);

            if (result)
            {
                // Повертаємо успішний результат з CreatedAtAction
                return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
            }

            return BadRequest("Error creating project.");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Project data cannot be null.");
            }
            
            // Перевірка на відповідність ID
            if (id != dto.Id)
            {
                return BadRequest("Project ID mismatch.");
            }

            // Створення об'єкта проекту для оновлення
            var project = new Project
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Budget = dto.Budget,
                Status = dto.Status,
                Category = dto.Category,
                CustomerId = dto.CustomerId,
                ExecutorId = dto.ExecutorId
            };

            var result = await _projectService.UpdateProjectAsync(project);
            if (result)
            {
                return Ok(project);  // Повертаємо оновлений проект
            }

            else { return NotFound("Project not found or update not allowed."); }
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
