using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitect.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "7e0f23bd-8494-4da9-8765-9fdcde966030",
                    Email = "employee@email.com",
                    NormalizedEmail = "EMPLOYEE@EMAIL.COM",
                    FirstName = "Emp",
                    LastName = "Loyee",
                    UserName = "employee@email.com",
                    NormalizedUserName = "EMPLOYEE@EMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd123"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "63723ff3-7de4-48b1-b96c-677a242fdeab",
                    Email = "admin@email.com",
                    NormalizedEmail = "ADMIN@EMAIL.COM",
                    FirstName = "Ad",
                    LastName = "Min",
                    UserName = "admin@email.com",
                    NormalizedUserName = "ADMIN@EMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd123"),
                    EmailConfirmed = true
                });
        }
    }
}
