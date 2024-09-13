using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task.Models;
using Task.Settings;

namespace Task.Data
{
    public static class ModelBuilderExtensions
    {     

		public static void SeedUserAndRoles(this ModelBuilder modelBuilder)
        {

            // Add Roles
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                 new IdentityRole { Name = FileSettings.AdminRole, NormalizedName = "ADMIN" },
                 new IdentityRole { Name = FileSettings.UserRole, NormalizedName = "USER" }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
            
            // Seed Users
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                 new ApplicationUser {
                    FullName = "Mohanad Wael",
                    UserName = "mohanadwael",
                    NormalizedUserName = "MOHANAD_WAEL",
                    Email = "mohanedwael16@gmail.com",
                    NormalizedEmail = "MOHANEDWAEL16@GMAIL.COM",
                },

                new ApplicationUser {
                    FullName = "Student User",
                    UserName = "student_user",
                    NormalizedUserName = "STUDENT_USER",
                    Email = "student_user@gmail.com",
                    NormalizedEmail = "STUDENT_USER@GMAIL.COM",
                },
            };
            modelBuilder.Entity<ApplicationUser>().HasData(users);

            // Seed UserRoles
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            // Add Password For All Users
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Mohanad_123");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Student_123");

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId =
            roles.First(q => q.Name == FileSettings.AdminRole).Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId =
            roles.First(q => q.Name == FileSettings.UserRole).Id
            });


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}
