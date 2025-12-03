using CellTracker.Api.Auth;
using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CellTracker.Api.Configuration.Extension
{
    public static class DatabaseExtension
    {

        public static IServiceCollection RegisterDbContextExtension(
            this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(ConnectionConfiguration.GetConnectionString(),
                npsqlOptions => npsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Application))
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning))
            );

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseNpgsql(ConnectionConfiguration.GetConnectionString(),
                npsqlOptions => npsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Identity))
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning))
            );

            return services;
        }

        public static async Task SeedInitialDataAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            RoleManager<IdentityRole> roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            try
            {
                if (!await roleManager.RoleExistsAsync(Roles.Operator))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Operator));
                }
                if (!await roleManager.RoleExistsAsync(Roles.Manager))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Manager));
                }
                if (!await roleManager.RoleExistsAsync(Roles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                }

                app.Logger.LogInformation("Successfully created roles.");

                await SeedInitialusersAsync(scope);


            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "An error occurred while seeding initial data.");
                throw;
            }
        }

        private static async Task SeedInitialusersAsync(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<SiteUser>>();

            var usersToCreate = new List<SiteUser>
            {
                new SiteUser { Id = "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5", FirstName = "Test", LastName = "User", UserName = "testuser", NormalizedUserName = "TESTUSER", Email = "test.user@example.com", NormalizedEmail = "TEST.USER@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "ee6739f0-97e2-496c-8ffe-74a39ae7a8e4", PasswordHash = "1111111", PhoneNumber="1000000000", PhoneNumberConfirmed=true, LoginCode="9000000000" },
                new SiteUser { Id = "35031d70-8287-4bfe-bd63-05a816f44885", FirstName = "Test1", LastName = "User1", UserName = "testuser1", NormalizedUserName = "TESTUSER1", Email = "test.user1@example.com", NormalizedEmail = "TEST.USER1@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "35031d70-8287-4bfe-bd63-05a816f44880", PasswordHash = "0000000", PhoneNumber="1000000001", PhoneNumberConfirmed=true, LoginCode="9000000001" },
                new SiteUser { Id = "11111111-1111-1111-1111-111111111111", FirstName = "John", LastName = "Smith", UserName = "jsmith", NormalizedUserName = "JSMITH", Email = "john.smith@example.com", NormalizedEmail = "JOHN.SMITH@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "11111111-1111-1111-1111-111111111110", PasswordHash = "hash1", PhoneNumber="1000000002", PhoneNumberConfirmed=true, LoginCode="9000000002" },
                new SiteUser { Id = "22222222-2222-2222-2222-222222222222", FirstName = "Emily", LastName = "Clark", UserName = "eclark", NormalizedUserName = "ECLARK", Email = "emily.clark@example.com", NormalizedEmail = "EMILY.CLARK@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "22222222-2222-2222-2222-222222222221", PasswordHash = "hash2", PhoneNumber="1000000003", PhoneNumberConfirmed=true, LoginCode="9000000003" },
                new SiteUser { Id = "33333333-3333-3333-3333-333333333333", FirstName = "Michael", LastName = "Davis", UserName = "mdavis", NormalizedUserName = "MDAVIS", Email = "michael.davis@example.com", NormalizedEmail = "MICHAEL.DAVIS@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "33333333-3333-3333-3333-333333333332", PasswordHash = "hash3", PhoneNumber="1000000004", PhoneNumberConfirmed=true, LoginCode="9000000004" },
                new SiteUser { Id = "44444444-4444-4444-4444-444444444444", FirstName = "Sarah", LastName = "Wilson", UserName = "swilson", NormalizedUserName = "SWILSON", Email = "sarah.wilson@example.com", NormalizedEmail = "SARAH.WILSON@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "44444444-4444-4444-4444-444444444443", PasswordHash = "hash4", PhoneNumber="1000000005", PhoneNumberConfirmed=true, LoginCode="9000000005" },
                new SiteUser { Id = "55555555-5555-5555-5555-555555555555", FirstName = "David", LastName = "Miller", UserName = "dmiller", NormalizedUserName = "DMILLER", Email = "david.miller@example.com", NormalizedEmail = "DAVID.MILLER@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "55555555-5555-5555-5555-555555555554", PasswordHash = "hash5", PhoneNumber="1000000006", PhoneNumberConfirmed=true, LoginCode="9000000006" },
                new SiteUser { Id = "66666666-6666-6666-6666-666666666666", FirstName = "Olivia", LastName = "Taylor", UserName = "otaylor", NormalizedUserName = "OTAYLOR", Email = "olivia.taylor@example.com", NormalizedEmail = "OLIVIA.TAYLOR@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "66666666-6666-6666-6666-666666666665", PasswordHash = "hash6", PhoneNumber="1000000007", PhoneNumberConfirmed=true, LoginCode="9000000007" },
                new SiteUser { Id = "77777777-7777-7777-7777-777777777777", FirstName = "Daniel", LastName = "Anderson", UserName = "danderson", NormalizedUserName = "DANDERSON", Email = "daniel.anderson@example.com", NormalizedEmail = "DANIEL.ANDERSON@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "77777777-7777-7777-7777-777777777776", PasswordHash = "hash7", PhoneNumber="1000000008", PhoneNumberConfirmed=true, LoginCode="9000000008" },
                new SiteUser { Id = "88888888-8888-8888-8888-888888888888", FirstName = "Sophia", LastName = "Thomas", UserName = "sthomas", NormalizedUserName = "STHOMAS", Email = "sophia.thomas@example.com", NormalizedEmail = "SOPHIA.THOMAS@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "88888888-8888-8888-8888-888888888887", PasswordHash = "hash8", PhoneNumber="1000000009", PhoneNumberConfirmed=true, LoginCode="9000000009" },
                new SiteUser { Id = "99999999-9999-9999-9999-999999999999", FirstName = "Liam", LastName = "Wright", UserName = "lwright", NormalizedUserName = "LWRIGHT", Email = "liam.wright@example.com", NormalizedEmail = "LIAM.WRIGHT@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "99999999-9999-9999-9999-999999999998", PasswordHash = "hash9", PhoneNumber="1000000010", PhoneNumberConfirmed=true, LoginCode="9000000010" },
                new SiteUser { Id = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", FirstName = "Ava", LastName = "Hall", UserName = "ahall", NormalizedUserName = "AHALL", Email = "ava.hall@example.com", NormalizedEmail = "AVA.HALL@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab", PasswordHash = "hash10", PhoneNumber="1000000011", PhoneNumberConfirmed=true, LoginCode="9000000011" },
                new SiteUser { Id = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", FirstName = "Noah", LastName = "Young", UserName = "nyoung", NormalizedUserName = "NYOUNG", Email = "noah.young@example.com", NormalizedEmail = "NOAH.YOUNG@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbc", PasswordHash = "hash11", PhoneNumber="1000000012", PhoneNumberConfirmed=true, LoginCode="9000000012" },
                new SiteUser { Id = "cccccccc-cccc-cccc-cccc-cccccccccccc", FirstName = "Isabella", LastName = "King", UserName = "iking", NormalizedUserName = "IKING", Email = "isabella.king@example.com", NormalizedEmail = "ISABELLA.KING@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "cccccccc-cccc-cccc-cccc-cccccccccccd", PasswordHash = "hash12", PhoneNumber="1000000013", PhoneNumberConfirmed=true, LoginCode="9000000013" },
                new SiteUser { Id = "dddddddd-dddd-dddd-dddd-dddddddddddd", FirstName = "James", LastName = "Scott", UserName = "jscott", NormalizedUserName = "JSCOTT", Email = "jscott@example.com", NormalizedEmail = "JS123@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "dddddddd-dddd-dddd-dddd-ddddddddddde", PasswordHash = "hash13", PhoneNumber="1000000014", PhoneNumberConfirmed=true, LoginCode="9000000014" },
                new SiteUser { Id = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", FirstName = "Mia", LastName = "Green", UserName = "mgreen", NormalizedUserName = "MGREEN", Email = "mia.green@example.com", NormalizedEmail = "MIA.GREEN@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeef", PasswordHash = "hash14", PhoneNumber="1000000015", PhoneNumberConfirmed=true, LoginCode="9000000015" },
                new SiteUser { Id = "ffffffff-ffff-ffff-ffff-ffffffffffff", FirstName = "Benjamin", LastName = "Adams", UserName = "badams", NormalizedUserName = "BADAMS", Email = "benjamin.adams@example.com", NormalizedEmail = "BENJAMIN.ADAMS@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "ffffffff-ffff-ffff-ffff-fffffffffffe", PasswordHash = "hash15", PhoneNumber="1000000016", PhoneNumberConfirmed=true, LoginCode="9000000016" },
                new SiteUser { Id = "12121212-1212-1212-1212-121212121212", FirstName = "Charlotte", LastName = "Baker", UserName = "cbaker", NormalizedUserName = "CBAKER", Email = "charlotte.baker@example.com", NormalizedEmail = "CHARLOTTE.BAKER@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "12121212-1212-1212-1212-121212121213", PasswordHash = "hash16", PhoneNumber="1000000017", PhoneNumberConfirmed=true, LoginCode="9000000017" },
                new SiteUser { Id = "13131313-1313-1313-1313-131313131313", FirstName = "Elijah", LastName = "Nelson", UserName = "enelson", NormalizedUserName = "ENELSON", Email = "elijah.nelson@example.com", NormalizedEmail = "ELIJAH.NELSON@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "13131313-1313-1313-1313-131313131314", PasswordHash = "hash17", PhoneNumber="1000000018", PhoneNumberConfirmed=true, LoginCode="9000000018" },
                new SiteUser { Id = "14141414-1414-1414-1414-141414141414", FirstName = "Amelia", LastName = "Carter", UserName = "acarter", NormalizedUserName = "ACARTER", Email = "amelia.carter@example.com", NormalizedEmail = "AMELIA.CARTER@EXAMPLE.COM", EmailConfirmed = true, SecurityStamp = "14141414-1414-1414-1414-141414141415", PasswordHash = "hash18", PhoneNumber="1000000019", PhoneNumberConfirmed=true, LoginCode="9000000019" }
            };


            for (int i = 0; i < usersToCreate.Count; i++)
            {
                var user = usersToCreate[i];

                var role =
                    i < 5 ? Roles.Admin :
                    i < 10 ? Roles.Manager :
                    Roles.Operator;

                if (await userManager.FindByIdAsync(user.Id) == null)
                {
                    await userManager.CreateAsync(user);
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
