using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelloSocNetw_DAL.Identity;
using System;

namespace HelloSocNetw_DAL
{
    public class SocNetwContext : IdentityDbContext<AppIdentityUser, AppRole,
        Guid, IdentityUserClaim<Guid>,
        AppUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public SocNetwContext(DbContextOptions<SocNetwContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<UserInfo> UsersInfo { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<RepairRequest> RepairRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //applies all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocNetwContext).Assembly);
        }
    }
}