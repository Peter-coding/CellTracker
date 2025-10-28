using CellTracker.Api.Auth;
using CellTracker.Api.Models;
using CellTracker.Api.Models.OperatorTask;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CellTracker.Api.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // TODO class DbSets<>
        public DbSet<OperatorTask> OperatorTasks { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<WorkStation> WorkStations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(Schemas.Application);

            base.OnModelCreating(builder);

            // --- Seed OperatorTask ---
            builder.Entity<OperatorTask>().HasData(
                new OperatorTask
                {
                    Id = Guid.Parse("35031d70-8287-4bfe-bd63-05a816f44444"),
                    Name = "Test Task",
                    Description = "Sample operator task",
                    QuantityGoal = 10,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                }
            );
            builder.Entity<OperatorTask>().HasData(
                new OperatorTask
                {
                    Id = Guid.Parse("35031d70-8287-4bfe-bd63-05a816f44666"),
                    Name = "Test Task2",
                    Description = "Sample operator task2",
                    QuantityGoal = 15,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                }
            );

        }

    }
}
