using CellTracker.Api.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Data
{
    public sealed class AppIdentityDbContext : IdentityDbContext<SiteUser>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(Schemas.Identity);

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
            //user.PasswordHash = hasher.HashPassword(user, "Test123!2");

            builder.Entity<SiteUser>().HasData(user);
        }
    }
}
