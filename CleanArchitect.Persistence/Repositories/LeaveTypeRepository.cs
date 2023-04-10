using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Domain;

namespace CleanArchitect.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(CleanArchitectDbContext dbContext) : base(dbContext)
        {
        }
    }
}
