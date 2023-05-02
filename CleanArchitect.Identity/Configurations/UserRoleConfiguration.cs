using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitect.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "bdc8612e-931a-495d-a54c-d35fc8518cd0",
                    UserId = "7e0f23bd-8494-4da9-8765-9fdcde966030"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "3d321718-a7eb-4c8a-b469-edcc169077c5",
                    UserId = "63723ff3-7de4-48b1-b96c-677a242fdeab"
                }
                );
        }
    }
}
