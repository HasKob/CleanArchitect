using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Domain;
using CleanArchitect.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitect.Persistence
{
    public class CleanArchitectDbContext : DbContext
    {
        public CleanArchitectDbContext(DbContextOptions<CleanArchitectDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchitectDbContext).Assembly);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<LeaveType> LeaveTypes {  get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations {  get; set; }
        public DbSet<LeaveRequest> LeaveRequests {  get; set; }

    }
}
