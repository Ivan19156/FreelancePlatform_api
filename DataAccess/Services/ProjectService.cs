using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Date;
using DataAccess.Models;

namespace DataAccess.Services
{
    public class ProjectService : IProjectService
    {
        private readonly FreelancePlatformDbContext _context;

        public ProjectService(FreelancePlatformDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProjectAsync(Project project)
        {
            if (project == null) return false;
            await _context.Projects.AddAsync(project);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool CanBeUpdated(Project project)
        {
            var existingProject = _context.Projects.AsNoTracking().SingleOrDefault(u => u.Id == project.Id);
            return !string.IsNullOrEmpty(project.Name) &&
                   project.Name != existingProject.Name &&
                   !string.IsNullOrEmpty(project.Description) &&
                   project.Description != existingProject.Description;
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            var existingProject = await _context.Projects.SingleOrDefaultAsync(u => u.Id == project.Id);
            if (existingProject == null) return false;

            if (CanBeUpdated(project))
            {
                existingProject.Name = project.Name;
                existingProject.Description = project.Description;
                existingProject.Budget = project.Budget;
                existingProject.Status = project.Status;
                existingProject.Category = project.Category;
                existingProject.CustomerId = project.CustomerId;
                existingProject.ExecutorId = project.ExecutorId;

                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == projectId);
            if (project == null) return false;

            _context.Projects.Remove(project);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Project>> GetProjectsByCustomerAsync(int customerId)
        {
            return await _context.Projects.Where(p => p.CustomerId == customerId).ToListAsync();
        }

        public async Task<bool> AssignFreelancerAsync(int projectId, int freelancerId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null) return false;

            var freelancer = await _context.Users.FirstOrDefaultAsync(u => u.Id == freelancerId);
            if (freelancer == null) return false;

            project.Executor = freelancer;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
