using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using HelloSocNetw_DAL.Infrastructure.Attributes;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Interfaces;
using System.Threading;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using HelloSocNetw_DAL.Entities.IdentityEntities;
using HelloSocNetw_DAL.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelloSocNetw_DAL
{
    public class SocNetwContext : IdentityDbContext<
        AppIdentityUser,
        AppRole,
        Guid,
        IdentityUserClaim<Guid>,
        AppUserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        public SocNetwContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<UserInfo> UsersInfo { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<RepairRequest> RepairRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ShadowPropertiesService.ConfigureShadowProperties(modelBuilder);
            QueryFiltersService.ConfigureQueryFilters(modelBuilder);

            base.OnModelCreating(modelBuilder);

            //applies all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocNetwContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var allEntityEntries = ChangeTracker.Entries();

            ShadowPropertiesService.SetShadowPropertiesValues(allEntityEntries);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}