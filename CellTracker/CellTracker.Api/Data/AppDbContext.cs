using CellTracker.Api.Auth;
using CellTracker.Api.Models.OperatorTask;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Data
{
    public class AppDbContext : IdentityDbContext<SiteUser>
    {

        // TODO class DbSets<>
        public DbSet<OperatorTask> OperatorTasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- Seed OperatorTask ---
            builder.Entity<OperatorTask>().HasData(
                new OperatorTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Task",
                    Description = "Sample operator task",
                    QuantityGoal = 10,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                }
            );
            builder.Entity<OperatorTask>().HasData(
                new OperatorTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Task2",
                    Description = "Sample operator task2",
                    QuantityGoal = 15,
                    CreatedAt = new DateTime(2025, 01, 02, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // --- Seed SiteUser ---
            var hasher = new PasswordHasher<SiteUser>();
            var user = new SiteUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "User",
                UserName = "testuser",
                NormalizedUserName = "TESTUSER",
                Email = "test.user@example.com",
                NormalizedEmail = "TEST.USER@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };
            user.PasswordHash = hasher.HashPassword(user, "Test123!");

            builder.Entity<SiteUser>().HasData(user);

            user = new SiteUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test1",
                LastName = "User1",
                UserName = "testuser1",
                NormalizedUserName = "TESTUSER1",
                Email = "test.user1@example.com",
                NormalizedEmail = "TEST.USER1@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };
            user.PasswordHash = hasher.HashPassword(user, "Test123!2");

            builder.Entity<SiteUser>().HasData(user);
        }

    }
}
