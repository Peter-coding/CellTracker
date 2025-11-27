using CellTracker.Api.Auth;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Models.Simulation;
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
        public DbSet<SimulationModel> Simulations { get; set; }
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

            // --- Seed Factory, ProductionLine, Cell, WorkStation, OperatorTask ---



            var factory1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var factory2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var factory3Id = Guid.Parse("33333333-aaaa-bbbb-cccc-111111111111");
            var factory4Id = Guid.Parse("44444444-aaaa-bbbb-cccc-222222222222");

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
                },
                new Factory
                {
                    Id = factory3Id,
                    Name = "Electronics Plant",
                    Country = "Germany",
                    City = "Stuttgart",
                    Address = "Werkstrasse 8",
                    Email = "contact@electronicsplant.de",
                    Phone = "+49 711 123 456"
                },
                new Factory
                {
                    Id = factory4Id,
                    Name = "Automation Hub",
                    Country = "Czech Republic",
                    City = "Brno",
                    Address = "Technicka 22",
                    Email = "info@automationhub.cz",
                    Phone = "+420 555 987 654"
                }
            );

            var line1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var line2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var line3Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var line4Id = Guid.Parse("66666666-aaaa-bbbb-cccc-111111111111");
            var line5Id = Guid.Parse("77777777-aaaa-bbbb-cccc-111111111111");
            var line6Id = Guid.Parse("88888888-aaaa-bbbb-cccc-111111111111");
            var line7Id = Guid.Parse("99999999-aaaa-bbbb-cccc-222222222222");
            var line8Id = Guid.Parse("aaaaaaaa-aaaa-bbbb-cccc-222222222222");
            var line9Id = Guid.Parse("bbbbbbbb-aaaa-bbbb-cccc-222222222222");

            builder.Entity<ProductionLine>().HasData(
                new ProductionLine { Id = line1Id, Name = "Assembly Line A", OrdinalNumber = 1, Description = "Automotive ECU assembly line", FactoryId = factory1Id, Status = ProductionLineStatus.Running },
                new ProductionLine { Id = line2Id, Name = "Assembly Line B", OrdinalNumber = 2, Description = "Battery module production", FactoryId = factory1Id, Status = ProductionLineStatus.Maintenance },
                new ProductionLine { Id = line3Id, Name = "Packaging Line 1", OrdinalNumber = 3, Description = "Final packaging and quality control", FactoryId = factory2Id, Status = ProductionLineStatus.Stopped },
                new ProductionLine { Id = line4Id, Name = "PCB Line 1", OrdinalNumber = 4, Description = "Motherboard soldering and assembly", FactoryId = factory3Id, Status = ProductionLineStatus.Running },
                new ProductionLine { Id = line5Id, Name = "PCB Line 2", OrdinalNumber = 5, Description = "High-density PCB assembly", FactoryId = factory3Id, Status = ProductionLineStatus.Maintenance },
                new ProductionLine { Id = line6Id, Name = "Final Assembly Line", OrdinalNumber = 6, Description = "Electronic unit final assembly", FactoryId = factory3Id, Status = ProductionLineStatus.Running },
                new ProductionLine { Id = line7Id, Name = "Automation Line 1", OrdinalNumber = 7, Description = "Industrial robot assembly", FactoryId = factory4Id, Status = ProductionLineStatus.Stopped },
                new ProductionLine { Id = line8Id, Name = "Automation Line 2", OrdinalNumber = 8, Description = "Robot calibration and testing line", FactoryId = factory4Id, Status = ProductionLineStatus.Running },
                new ProductionLine { Id = line9Id, Name = "Automation Line 3", OrdinalNumber = 9, Description = "AGV control unit production", FactoryId = factory4Id, Status = ProductionLineStatus.Maintenance }
            );


            Guid NewId(string seed) => Guid.Parse(seed);

            var cell1 = NewId("66666666-6666-6666-6666-666666666666");
            var cell2 = NewId("77777777-7777-7777-7777-777777777777");
            var cell3 = NewId("88888888-8888-8888-8888-888888888888");
            var cell4 = NewId("88888888-8888-8888-8888-888888888881");
            var cell5 = NewId("88888888-8888-8888-8888-888888888882");
            var cell6 = NewId("88888888-8888-8888-8888-888888888883");


            var pcbCell1 = NewId("11111111-aaaa-bbbb-cccc-111111111111");
            var pcbCell2 = NewId("11111111-bbbb-cccc-dddd-111111111111");
            var pcbCell3 = NewId("11111111-cccc-dddd-eeee-111111111111");

            var pcbCell4 = NewId("22222222-aaaa-bbbb-cccc-111111111111");
            var pcbCell5 = NewId("22222222-bbbb-cccc-dddd-111111111111");
            var pcbCell6 = NewId("22222222-cccc-dddd-eeee-111111111111");

            var finalCell1 = NewId("33333333-aaaa-bbbb-cccc-111111111111");
            var finalCell2 = NewId("33333333-bbbb-cccc-dddd-111111111111");
            var finalCell3 = NewId("33333333-cccc-dddd-eeee-111111111111");

            // Factory 4 cells
            var autoCell1 = NewId("44444444-aaaa-bbbb-cccc-222222222222");
            var autoCell2 = NewId("44444444-bbbb-cccc-dddd-222222222222");
            var autoCell3 = NewId("44444444-cccc-dddd-eeee-222222222222");

            var autoCell4 = NewId("55555555-aaaa-bbbb-cccc-222222222222");
            var autoCell5 = NewId("55555555-bbbb-cccc-dddd-222222222222");
            var autoCell6 = NewId("55555555-cccc-dddd-eeee-222222222222");

            var autoCell7 = NewId("66666666-aaaa-bbbb-cccc-222222222222");
            var autoCell8 = NewId("66666666-bbbb-cccc-dddd-222222222222");
            var autoCell9 = NewId("66666666-cccc-dddd-eeee-222222222222");

            builder.Entity<Cell>().HasData(
                new Cell { Id = cell1, Name = "Soldering Cell", OrdinalNumber = 1, Description = "Robotized soldering unit", ProductionLineId = line1Id },
                new Cell { Id = cell2, Name = "Inspection Cell", OrdinalNumber = 2, Description = "Optical quality inspection station", ProductionLineId = line1Id },
                new Cell { Id = cell3, Name = "Packaging Cell1", OrdinalNumber = 3, Description = "Box sealing and labeling unit1", ProductionLineId = line1Id },
                new Cell { Id = cell4, Name = "Packaging Cell2", OrdinalNumber = 4, Description = "Box sealing and labeling unit2", ProductionLineId = line1Id },
                new Cell { Id = cell5, Name = "Packaging Cell3", OrdinalNumber = 5, Description = "Box sealing and labeling unit3", ProductionLineId = line1Id },
                new Cell { Id = cell6, Name = "Packaging Cell4", OrdinalNumber = 6, Description = "Box sealing and labeling unit4", ProductionLineId = line1Id },

                // PCB Line 1
                new Cell { Id = pcbCell1, Name = "Solder Prep", OrdinalNumber = 1, Description = "Preparation", ProductionLineId = line4Id },
                new Cell { Id = pcbCell2, Name = "Soldering", OrdinalNumber = 2, Description = "Wave solder station", ProductionLineId = line4Id },
                new Cell { Id = pcbCell3, Name = "Cleaning", OrdinalNumber = 3, Description = "Flux removal", ProductionLineId = line4Id },

                // PCB Line 2
                new Cell { Id = pcbCell4, Name = "Pick&Place", OrdinalNumber = 1, Description = "Chip placement", ProductionLineId = line5Id },
                new Cell { Id = pcbCell5, Name = "Reflow", OrdinalNumber = 2, Description = "Overheating tunnel", ProductionLineId = line5Id },
                new Cell { Id = pcbCell6, Name = "AOI", OrdinalNumber = 3, Description = "Optical inspection", ProductionLineId = line5Id },

                // Final Assembly
                new Cell { Id = finalCell1, Name = "Housing Assembly", OrdinalNumber = 1, Description = "Chassis assembly", ProductionLineId = line6Id },
                new Cell { Id = finalCell2, Name = "Firmware Load", OrdinalNumber = 2, Description = "Firmware flashing", ProductionLineId = line6Id },
                new Cell { Id = finalCell3, Name = "Final QC", OrdinalNumber = 3, Description = "Final quality control", ProductionLineId = line6Id },

                // Factory 4 lines
                new Cell { Id = autoCell1, Name = "Robot Arm Assembly", OrdinalNumber = 1, Description = "Arm modules", ProductionLineId = line7Id },
                new Cell { Id = autoCell2, Name = "Motor Assembly", OrdinalNumber = 2, Description = "Servo motors", ProductionLineId = line7Id },
                new Cell { Id = autoCell3, Name = "Safety Check", OrdinalNumber = 3, Description = "Safety certification", ProductionLineId = line7Id },

                new Cell { Id = autoCell4, Name = "Calibration Base", OrdinalNumber = 1, Description = "Initial calibration", ProductionLineId = line8Id },
                new Cell { Id = autoCell5, Name = "Vision Alignment", OrdinalNumber = 2, Description = "Camera alignment", ProductionLineId = line8Id },
                new Cell { Id = autoCell6, Name = "Stress Test", OrdinalNumber = 3, Description = "Mechanical testing", ProductionLineId = line8Id },

                new Cell { Id = autoCell7, Name = "AGV Control Board", OrdinalNumber = 1, Description = "PCB assembly", ProductionLineId = line9Id },
                new Cell { Id = autoCell8, Name = "Lidar Install", OrdinalNumber = 2, Description = "Sensors installation", ProductionLineId = line9Id },
                new Cell { Id = autoCell9, Name = "Navigation Test", OrdinalNumber = 3, Description = "AGV nav calibration", ProductionLineId = line9Id }
            );

            builder.Entity<WorkStation>().HasData(
                new WorkStation { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "WS-SLD-01", MqttDeviceId = "A1", OrdinalNumber = 1, Description = "Manual soldering station", CellId = cell1 },
                new WorkStation { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "WS-SLD-02", MqttDeviceId = "A2", OrdinalNumber = 2, Description = "Automated soldering robot arm", CellId = cell1 },
                new WorkStation { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "WS-SLD-03", MqttDeviceId = "A3", OrdinalNumber = 3, Description = "Precision soldering microstation", CellId = cell1 },
                new WorkStation { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Name = "WS-SLD-04", MqttDeviceId = "A4", OrdinalNumber = 4, Description = "High-temperature soldering unit", CellId = cell1 },
                new WorkStation { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), Name = "WS-SLD-05", MqttDeviceId = "A5", OrdinalNumber = 5, Description = "Flux application station", CellId = cell1 },
                new WorkStation { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), Name = "WS-SLD-06", MqttDeviceId = "A6", OrdinalNumber = 6, Description = "Reflow soldering preparation", CellId = cell1 },
                new WorkStation { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), Name = "WS-SLD-07", MqttDeviceId = "A7", OrdinalNumber = 7, Description = "Post-soldering cooling point", CellId = cell1 },

                new WorkStation { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "WS-INS-01", MqttDeviceId = "A8", OrdinalNumber = 1, Description = "Visual inspection camera station", CellId = cell2 },
                new WorkStation { Id = Guid.Parse("22222222-2222-2222-2222-222222222221"), Name = "WS-INS-02", MqttDeviceId = "A9", OrdinalNumber = 2, Description = "AOI optical scan unit", CellId = cell2 },
                new WorkStation { Id = Guid.Parse("22222222-2222-2222-2222-222222222223"), Name = "WS-INS-04", MqttDeviceId = "A10", OrdinalNumber = 3, Description = "Automated dimension check sensor", CellId = cell2 },
                new WorkStation { Id = Guid.Parse("22222222-2222-2222-2222-222222222224"), Name = "WS-INS-05", MqttDeviceId = "A11", OrdinalNumber = 4, Description = "Barcode and label verification", CellId = cell2 },
                new WorkStation { Id = Guid.Parse("22222222-2222-2222-2222-222222222225"), Name = "WS-INS-06", MqttDeviceId = "A12", OrdinalNumber = 5, Description = "Quality classification station", CellId = cell2 },
                new WorkStation { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "WS-INS-03", MqttDeviceId = "A13", OrdinalNumber = 6, Description = "High-resolution defect camera", CellId = cell2 },

                new WorkStation { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "WS-PKG-01", MqttDeviceId = "A14", OrdinalNumber = 1, Description = "Box assembly station", CellId = cell3 },
                new WorkStation { Id = Guid.Parse("33333333-3333-3333-3333-333333333332"), Name = "WS-PKG0-02", MqttDeviceId = "A15", OrdinalNumber = 2, Description = "Filling alignment station", CellId = cell3 },
                new WorkStation { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "WS-PKG0-03", MqttDeviceId = "A16", OrdinalNumber = 3, Description = "Protective foam insertion unit", CellId = cell3 },
                new WorkStation { Id = Guid.Parse("33333333-3333-3333-3333-333333333334"), Name = "WS-PKG0-04", MqttDeviceId = "A17", OrdinalNumber = 4, Description = "Automated box sealing", CellId = cell3 },
                new WorkStation { Id = Guid.Parse("33333333-3333-3333-3333-333333333335"), Name = "WS-PKG0-05", MqttDeviceId = "A18", OrdinalNumber = 5, Description = "Package weight verification", CellId = cell3 },
                new WorkStation { Id = Guid.Parse("33333333-3333-3333-3333-333333333331"), Name = "WS-PKG0-01", MqttDeviceId = "A19", OrdinalNumber = 6, Description = "Automated carton forming", CellId = cell3 },

                new WorkStation { Id = Guid.Parse("44444444-4444-4444-4444-444444444441"), Name = "WS-PKG1-01", MqttDeviceId = "A20", OrdinalNumber = 1, Description = "Automated box printing", CellId = cell4 },
                new WorkStation { Id = Guid.Parse("44444444-4444-4444-4444-444444444442"), Name = "WS-PKG1-02", MqttDeviceId = "A21", OrdinalNumber = 2, Description = "RFID label application", CellId = cell4 },
                new WorkStation { Id = Guid.Parse("44444444-4444-4444-4444-444444444443"), Name = "WS-PKG1-03", MqttDeviceId = "A22", OrdinalNumber = 3, Description = "Automated shrink-wrap tunnel", CellId = cell4 },
                new WorkStation { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "WS-PKG1-04", MqttDeviceId = "A23", OrdinalNumber = 4, Description = "Parcel integrity check", CellId = cell4 },
                new WorkStation { Id = Guid.Parse("44444444-4444-4444-4444-444444444445"), Name = "WS-PKG1-05", MqttDeviceId = "A24", OrdinalNumber = 5, Description = "Sorting conveyor loader", CellId = cell4 },

                new WorkStation { Id = Guid.Parse("55555555-5555-5555-5555-555555555551"), Name = "WS-PKG2-01", MqttDeviceId = "A25", OrdinalNumber = 1, Description = "Tape dispensing system", CellId = cell5 },
                new WorkStation { Id = Guid.Parse("55555555-5555-5555-5555-555555555552"), Name = "WS-PKG2-02", MqttDeviceId = "A26", OrdinalNumber = 2, Description = "Automated barcode printer", CellId = cell5 },
                new WorkStation { Id = Guid.Parse("55555555-5555-5555-5555-555555555553"), Name = "WS-PKG2-03", MqttDeviceId = "A27", OrdinalNumber = 3, Description = "Package photo capture unit", CellId = cell5 },
                new WorkStation { Id = Guid.Parse("55555555-5555-5555-5555-555555555554"), Name = "WS-PKG2-04", MqttDeviceId = "A28", OrdinalNumber = 4, Description = "Load distribution analyzer", CellId = cell5 },
                new WorkStation { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "WS-PKG2-05", MqttDeviceId = "A29", OrdinalNumber = 5, Description = "Outgoing product staging", CellId = cell5 },

                new WorkStation { Id = Guid.Parse("66666666-6666-6666-6666-666666666661"), Name = "WS-PKG3-01", MqttDeviceId = "A30", OrdinalNumber = 1, Description = "Box folding robot arm", CellId = cell6 },
                new WorkStation { Id = Guid.Parse("66666666-6666-6666-6666-666666666662"), Name = "WS-PKG3-02", MqttDeviceId = "A31", OrdinalNumber = 2, Description = "Label placement verification", CellId = cell6 },
                new WorkStation { Id = Guid.Parse("66666666-6666-6666-6666-666666666663"), Name = "WS-PKG3-03", MqttDeviceId = "A32", OrdinalNumber = 3, Description = "Box pressure test unit", CellId = cell6 },
                new WorkStation { Id = Guid.Parse("66666666-6666-6666-6666-666666666664"), Name = "WS-PKG3-04", MqttDeviceId = "A33", OrdinalNumber = 4, Description = "Automated strapping machine", CellId = cell6 },
                new WorkStation { Id = Guid.Parse("66666666-6666-6666-6666-666666666665"), Name = "WS-PKG3-05", MqttDeviceId = "A34", OrdinalNumber = 5, Description = "Final pallet loading spot", CellId = cell6 },

                new WorkStation { Id = Guid.Parse("10000000-aaaa-bbbb-cccc-111111111111"), Name = "WS-SP-01", MqttDeviceId = "B1", OrdinalNumber = 1, Description = "Solder prep station #1", CellId = pcbCell1 },
                new WorkStation { Id = Guid.Parse("10000000-aaaa-bbbb-cccc-111111111112"), Name = "WS-SP-02", MqttDeviceId = "B2", OrdinalNumber = 2, Description = "Solder prep station #2", CellId = pcbCell1 },
                new WorkStation { Id = Guid.Parse("10000000-aaaa-bbbb-cccc-111111111113"), Name = "WS-SP-03", MqttDeviceId = "B3", OrdinalNumber = 3, Description = "Solder prep station #3", CellId = pcbCell1 },
                new WorkStation { Id = Guid.Parse("10000000-aaaa-bbbb-cccc-111111111114"), Name = "WS-SP-04", MqttDeviceId = "B4", OrdinalNumber = 4, Description = "Solder prep station #4", CellId = pcbCell1 },
                new WorkStation { Id = Guid.Parse("10000000-aaaa-bbbb-cccc-111111111115"), Name = "WS-SP-05", MqttDeviceId = "B5", OrdinalNumber = 5, Description = "Solder prep station #5", CellId = pcbCell1 },

                new WorkStation { Id = Guid.Parse("20000000-aaaa-bbbb-cccc-111111111111"), Name = "WS-SLD-01", MqttDeviceId = "B6", OrdinalNumber = 1, Description = "Wave solder #1", CellId = pcbCell2 },
                new WorkStation { Id = Guid.Parse("20000000-aaaa-bbbb-cccc-111111111112"), Name = "WS-SLD-02", MqttDeviceId = "B7", OrdinalNumber = 2, Description = "Wave solder #2", CellId = pcbCell2 },
                new WorkStation { Id = Guid.Parse("20000000-aaaa-bbbb-cccc-111111111113"), Name = "WS-SLD-03", MqttDeviceId = "B8", OrdinalNumber = 3, Description = "Wave solder #3", CellId = pcbCell2 },
                new WorkStation { Id = Guid.Parse("20000000-aaaa-bbbb-cccc-111111111114"), Name = "WS-SLD-04", MqttDeviceId = "B9", OrdinalNumber = 4, Description = "Wave solder #4", CellId = pcbCell2 },
                new WorkStation { Id = Guid.Parse("20000000-aaaa-bbbb-cccc-111111111115"), Name = "WS-SLD-05", MqttDeviceId = "B10", OrdinalNumber = 5, Description = "Wave solder #5", CellId = pcbCell2 },

                new WorkStation { Id = Guid.Parse("30000000-aaaa-bbbb-cccc-111111111111"), Name = "WS-CLN-01", MqttDeviceId = "B11", OrdinalNumber = 1, Description = "Cleaning station #1", CellId = pcbCell3 },
                new WorkStation { Id = Guid.Parse("30000000-aaaa-bbbb-cccc-111111111112"), Name = "WS-CLN-02", MqttDeviceId = "B12", OrdinalNumber = 2, Description = "Cleaning station #2", CellId = pcbCell3 },
                new WorkStation { Id = Guid.Parse("30000000-aaaa-bbbb-cccc-111111111113"), Name = "WS-CLN-03", MqttDeviceId = "B13", OrdinalNumber = 3, Description = "Cleaning station #3", CellId = pcbCell3 },
                new WorkStation { Id = Guid.Parse("30000000-aaaa-bbbb-cccc-111111111114"), Name = "WS-CLN-04", MqttDeviceId = "B14", OrdinalNumber = 4, Description = "Cleaning station #4", CellId = pcbCell3 },
                new WorkStation { Id = Guid.Parse("30000000-aaaa-bbbb-cccc-111111111115"), Name = "WS-CLN-05", MqttDeviceId = "B15", OrdinalNumber = 5, Description = "Cleaning station #5", CellId = pcbCell3 },


                // --- PCB Line 2 ---
                new WorkStation { Id = Guid.Parse("40000000-aaaa-bbbb-cccc-111111111111"), Name = "WS-PP-01", MqttDeviceId = "D1", OrdinalNumber = 1, Description = "Pick&Place #1", CellId = pcbCell4 },
                new WorkStation { Id = Guid.Parse("40000000-aaaa-bbbb-cccc-111111111112"), Name = "WS-PP-02", MqttDeviceId = "D2", OrdinalNumber = 2, Description = "Pick&Place #2", CellId = pcbCell4 },
                new WorkStation { Id = Guid.Parse("40000000-aaaa-bbbb-cccc-111111111113"), Name = "WS-PP-03", MqttDeviceId = "D3", OrdinalNumber = 3, Description = "Pick&Place #3", CellId = pcbCell4 },
                new WorkStation { Id = Guid.Parse("40000000-aaaa-bbbb-cccc-111111111114"), Name = "WS-PP-04", MqttDeviceId = "D4", OrdinalNumber = 4, Description = "Pick&Place #4", CellId = pcbCell4 },
                new WorkStation { Id = Guid.Parse("40000000-aaaa-bbbb-cccc-111111111115"), Name = "WS-PP-05", MqttDeviceId = "D5", OrdinalNumber = 5, Description = "Pick&Place #5", CellId = pcbCell4 },

                new WorkStation { Id = Guid.Parse("50000000-aaaa-bbbb-cccc-111111111111"), Name = "WS-RF-01", MqttDeviceId = "E1", OrdinalNumber = 1, Description = "Reflow oven #1", CellId = pcbCell5 },
                new WorkStation { Id = Guid.Parse("50000000-aaaa-bbbb-cccc-111111111112"), Name = "WS-RF-02", MqttDeviceId = "E2", OrdinalNumber = 2, Description = "Reflow oven #2", CellId = pcbCell5 },
                new WorkStation { Id = Guid.Parse("50000000-aaaa-bbbb-cccc-111111111113"), Name = "WS-RF-03", MqttDeviceId = "E3", OrdinalNumber = 3, Description = "Reflow oven #3", CellId = pcbCell5 },
                new WorkStation { Id = Guid.Parse("50000000-aaaa-bbbb-cccc-111111111114"), Name = "WS-RF-04", MqttDeviceId = "E4", OrdinalNumber = 4, Description = "Reflow oven #4", CellId = pcbCell5 },
                new WorkStation { Id = Guid.Parse("50000000-aaaa-bbbb-cccc-111111111115"), Name = "WS-RF-05", MqttDeviceId = "E5", OrdinalNumber = 5, Description = "Reflow oven #5", CellId = pcbCell5 },

                new WorkStation { Id = Guid.Parse("60000000-aaaa-bbbb-cccc-111111111111"), Name = "WS-AOI-01", MqttDeviceId = "F1", OrdinalNumber = 1, Description = "AOI scanner #1", CellId = pcbCell6 },
                new WorkStation { Id = Guid.Parse("60000000-aaaa-bbbb-cccc-111111111112"), Name = "WS-AOI-02", MqttDeviceId = "F2", OrdinalNumber = 2, Description = "AOI scanner #2", CellId = pcbCell6 },
                new WorkStation { Id = Guid.Parse("60000000-aaaa-bbbb-cccc-111111111113"), Name = "WS-AOI-03", MqttDeviceId = "F3", OrdinalNumber = 3, Description = "AOI scanner #3", CellId = pcbCell6 },
                new WorkStation { Id = Guid.Parse("60000000-aaaa-bbbb-cccc-111111111114"), Name = "WS-AOI-04", MqttDeviceId = "F4", OrdinalNumber = 4, Description = "AOI scanner #4", CellId = pcbCell6 },
                new WorkStation { Id = Guid.Parse("60000000-aaaa-bbbb-cccc-111111111115"), Name = "WS-AOI-05", MqttDeviceId = "F5", OrdinalNumber = 5, Description = "AOI scanner #5", CellId = pcbCell6 }
            );

            builder.Entity<SimulationModel>().HasData(
                new SimulationModel { Id = Guid.Parse("20000000-0000-0000-0000-000000000001"), Shift = Shift.Morning, NumberOfProductsMade = 145, MinutesOfSimulation = 1, ProductionLineId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
                new SimulationModel { Id = Guid.Parse("20000000-0000-0000-0000-000000000002"), Shift = Shift.Afternoon, NumberOfProductsMade = 198, MinutesOfSimulation = 2, ProductionLineId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
                new SimulationModel { Id = Guid.Parse("20000000-0000-0000-0000-000000000003"), Shift = Shift.Night, NumberOfProductsMade = 170, MinutesOfSimulation = 3, ProductionLineId = Guid.Parse("44444444-4444-4444-4444-444444444444") },
                new SimulationModel { Id = Guid.Parse("20000000-0000-0000-0000-000000000004"), Shift = Shift.Morning, NumberOfProductsMade = 220, MinutesOfSimulation = 4, ProductionLineId = Guid.Parse("44444444-4444-4444-4444-444444444444") },
                new SimulationModel { Id = Guid.Parse("20000000-0000-0000-0000-000000000005"), Shift = Shift.Afternoon, NumberOfProductsMade = 260, MinutesOfSimulation = 5, ProductionLineId = Guid.Parse("55555555-5555-5555-5555-555555555555") }
            );

            builder.Entity<OperatorTask>().HasData(
                new OperatorTask
                {
                    Id = Guid.Parse("80000000-0000-0000-0000-000000000001"),
                    Name = "Initial Setup",
                    Description = "Prepare workstation tools and materials",
                    QuantityGoal = 50,
                    WorkStationId = Guid.Parse("99999999-9999-9999-9999-999999999999") 
                },
                new OperatorTask
                {
                    Id = Guid.Parse("80000000-0000-0000-0000-000000000002"),
                    Name = "Component Processing",
                    Description = "Handle and prepare incoming components",
                    QuantityGoal = 120,
                    WorkStationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                },
                new OperatorTask
                {
                    Id = Guid.Parse("80000000-0000-0000-0000-000000000003"),
                    Name = "Quality Check",
                    Description = "Perform visual and functional inspection",
                    QuantityGoal = 80,
                    WorkStationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") 
                },
                new OperatorTask
                {
                    Id = Guid.Parse("80000000-0000-0000-0000-000000000004"),
                    Name = "Package Preparation",
                    Description = "Prepare boxes and packaging materials",
                    QuantityGoal = 200,
                    WorkStationId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc") 
                },
                new OperatorTask
                {
                    Id = Guid.Parse("80000000-0000-0000-0000-000000000005"),
                    Name = "Final Assembly",
                    Description = "Assemble final product parts",
                    QuantityGoal = 150,
                    WorkStationId = Guid.Parse("10000000-aaaa-bbbb-cccc-111111111111") 
                }
            );


        }
    }
}
