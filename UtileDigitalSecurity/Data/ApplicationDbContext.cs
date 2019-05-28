using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UtileDigitalSecurity.Models;

namespace UtileDigitalSecurity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,long>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users").HasKey(u => new { u.Id });
            builder.Entity<ApplicationRole>().ToTable("Roles").HasKey(u=>new { u.Id});
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogin")
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });

            builder.Entity<IdentityUserRole<long>>().ToTable("UserRole").HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}
