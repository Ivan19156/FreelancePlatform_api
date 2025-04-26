using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(Project project);
        Task<bool> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int projectId);
        Task<List<Project>> GetProjectsByCustomerAsync(int customerId);
        Task<bool> AssignFreelancerAsync(int projectId, int freelancerId);
    }
}
