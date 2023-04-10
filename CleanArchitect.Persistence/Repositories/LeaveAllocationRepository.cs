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
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly CleanArchitectDbContext _dbContext;
        public LeaveAllocationRepository(CleanArchitectDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = _dbContext.LeaveAllocations
                .Include(e => e.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public Task<LeaveAllocation> GetLeaveAllocationWithDetails(int Id)
        {
            var leaveAllocation = _dbContext.LeaveAllocations.Include(e => e.LeaveType).FirstOrDefaultAsync(e => e.Id == Id);
            return leaveAllocation;
        }
    }
}
