using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Date;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
    public class RequestService : IRequestService
    {
        private readonly FreelancePlatformDbContext _context;

        public RequestService(FreelancePlatformDbContext context)
        {
            _context = context;
        }

        private async Task<bool> RequestExistAsync(Request request)
        {
            return await _context.Requests.AnyAsync(r => r.Id == request.Id);
        }

        public async Task<bool> SubmitRequestAsync(Request request)
        {
            if (await RequestExistAsync(request))
            {
                return false;  // Request already exists
            }

            await _context.Requests.AddAsync(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Request>> GetRequestsForProjectsAsync(int projectId)
        {
            return await _context.Requests
                .Where(r => r.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
