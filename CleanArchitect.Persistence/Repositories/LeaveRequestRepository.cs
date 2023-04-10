using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Persistence.Contracts;
using CleanArchitect.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitect.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly CleanArchitectDbContext _dbContext;
        public LeaveRequestRepository(CleanArchitectDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? Approved)
        {
            leaveRequest.Approved = Approved;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(e => e.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int Id)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(e => e.LeaveType)
                .FirstOrDefaultAsync(e => e.Id == Id);
            return leaveRequest;
        }
    }
}
