using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IRequestService
    {
        Task<bool> SubmitRequestAsync(Request request);
        Task<List<Request>> GetRequestsForProjectsAsync(int projectId);
    }
}

