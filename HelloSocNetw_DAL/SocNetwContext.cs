using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL
{
    public class SocNetwContext : IdentityDbContext<AppIdentityUser, AppUserRole, int>
    {
        public SocNetwContext(DbContextOptions<SocNetwContext> options)
            : base(options) => Database.EnsureCreated();

        public virtual DbSet<UserInfo> UsersInfo { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }

        //tables for MANY-TO-MANY relationships
        public virtual DbSet<FriendshipTable> Friends { get; set; }
        public virtual DbSet<SubscribersTable> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //applies all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocNetwContext).Assembly); 
        }
    }
}