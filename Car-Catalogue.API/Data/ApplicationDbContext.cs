using Car_Catalogue.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Car_Catalogue.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Car> Car { get; set; }

        private IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
            .HasData(new Car("Mitsubishi", "Lancer", 2020, 10000, "Evo", true, 100000));

            modelBuilder.Entity<Car>()
           .HasData(new Car("Subaru", "WRX", 2022, 10000, "STI", false, 300000));

            const string ADMIN_ID = "02174cf0–9412–4cfe - afbf - 59f706d72cf6";
            const string ROLE_ID = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6";

            //Seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //Create admin user
            var user = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "superadmin@admin.com",
                EmailConfirmed = true,
                UserName = "superadmin@admin.com",
                NormalizedUserName = "SUPERADMIN@ADMIN.COM"
            };

            //Set user password
            var hasher = new PasswordHasher<ApplicationUser>();
            hasher.HashPassword(user, "Admin@2024");

            //Seed user
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            //Set the role to the admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
