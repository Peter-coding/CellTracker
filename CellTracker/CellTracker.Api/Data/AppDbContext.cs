using CellTracker.Api.Auth;
using CellTracker.Api.Models.Configuration;
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
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(Schemas.Application);

            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).HasMaxLength(300);
                entity.Property(e => e.Token).HasMaxLength(1000);
                entity.HasIndex(e => e.Token).IsUnique();
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // --- Seed SiteUser ---
            var hasher = new PasswordHasher<SiteUser>();

            var user = new SiteUser
            {
                Id = "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                FirstName = "Test",
                LastName = "User",
                UserName = "testuser",
                NormalizedUserName = "TESTUSER",
                Email = "test.user@example.com",
                NormalizedEmail = "TEST.USER@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = "ee6739f0-97e2-496c-8ffe-74a39ae7a8e4",
                PasswordHash = "1111111"
            };
            builder.Entity<SiteUser>().HasData(user);

            user = new SiteUser
            {
                Id = "35031d70-8287-4bfe-bd63-05a816f44885",
                FirstName = "Test1",
                LastName = "User1",
                UserName = "testuser1",
                NormalizedUserName = "TESTUSER1",
                Email = "test.user1@example.com",
                NormalizedEmail = "TEST.USER1@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = "35031d70-8287-4bfe-bd63-05a816f44880",
                PasswordHash = "0000000"
            };
            builder.Entity<SiteUser>().HasData(user);

            // --- Seed Factory, ProductionLine, Cell, WorkStation ---

            var factory1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var factory2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");

            builder.Entity<Factory>().HasData(
                new Factory
                {
                    Id = factory1Id,
                    Name = "Main Factory",
                    Country = "Hungary",
                    City = "Budapest",
                    Address = "Industrial Park 12",
                    Email = "info@mainfactory.hu",
                    Phone = "+36 1 555 1234"
                },
                new Factory
                {
                    Id = factory2Id,
                    Name = "Backup Plant",
                    Country = "Hungary",
                    City = "Győr",
                    Address = "Technológia u. 4.",
                    Email = "contact@backupplant.hu",
                    Phone = "+36 96 444 777"
                }
            );

            var line1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var line2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var line3Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            builder.Entity<ProductionLine>().HasData(
                new ProductionLine
                {
                    Id = line1Id,
                    Name = "Assembly Line A",
                    OrdinalNumber = 1,
                    Description = "Automotive ECU assembly line",
                    FactoryId = factory1Id,
                    Status = ProductionLineStatus.Running
                },
                new ProductionLine
                {
                    Id = line2Id,
                    Name = "Assembly Line B",
                    OrdinalNumber = 2,
                    Description = "Battery module production",
                    FactoryId = factory1Id,
                    Status = ProductionLineStatus.Maintenance
                },
                new ProductionLine
                {
                    Id = line3Id,
                    Name = "Packaging Line 1",
                    OrdinalNumber = 1,
                    Description = "Final packaging and quality control",
                    FactoryId = factory2Id,
                    Status = ProductionLineStatus.Stopped
                }
            );

            var cell1Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var cell2Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var cell3Id = Guid.Parse("88888888-8888-8888-8888-888888888888");

            builder.Entity<Cell>().HasData(
                new Cell
                {
                    Id = cell1Id,
                    Name = "Soldering Cell",
                    OrdinalNumber = 1,
                    Description = "Robotized soldering unit",
                    ProductionLineId = line1Id
                },
                new Cell
                {
                    Id = cell2Id,
                    Name = "Inspection Cell",
                    OrdinalNumber = 2,
                    Description = "Optical quality inspection station",
                    ProductionLineId = line1Id
                },
                new Cell
                {
                    Id = cell3Id,
                    Name = "Packaging Cell",
                    OrdinalNumber = 1,
                    Description = "Box sealing and labeling unit",
                    ProductionLineId = line3Id
                }
            );

            builder.Entity<WorkStation>().HasData(
                new WorkStation
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Name = "WS-SLD-01",
                    MqttDeviceId = "A1",
                    OrdinalNumber = 1,
                    Description = "Manual soldering station",
                    CellId = cell1Id
                },
                new WorkStation
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "WS-SLD-02",
                    MqttDeviceId = "A2",
                    OrdinalNumber = 2,
                    Description = "Automated soldering robot arm",
                    CellId = cell1Id
                },
                new WorkStation
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Name = "WS-INS-01",
                    MqttDeviceId = "A3",
                    OrdinalNumber = 1,
                    Description = "Visual inspection camera station",
                    CellId = cell2Id
                },
                new WorkStation
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Name = "WS-PKG-01",
                    MqttDeviceId = "A4",
                    OrdinalNumber = 1,
                    Description = "Box assembly station",
                    CellId = cell3Id
                }
            );
        }


    }
}
