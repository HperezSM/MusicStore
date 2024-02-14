using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Entities.Info;
using System.Reflection;
//using MusicStore.

namespace MusicStore.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<MusicStoreUserIdentity>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ConcertInfo>().HasNoKey();
            modelBuilder.Entity<ReportInfo>().HasNoKey();

            modelBuilder.Entity<MusicStoreUserIdentity>(x => x.ToTable("User"));
            modelBuilder.Entity<IdentityRole>(x => x.ToTable("Role"));
            modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UserRole"));
        }
    }
}
