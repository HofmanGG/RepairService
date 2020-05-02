using System;
using System.Collections.Generic;
using System.Text;
using HelloSocNetw_DAL.Infrastructure.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelloSocNetw_DAL.Infrastructure.Services
{
    public static class ConfigureShadowPropertiesService
    {
        private static ModelBuilder _modelBuilder;

        private static IEnumerable<IMutableEntityType> AllMutableEntityTypes => _modelBuilder.Model.GetEntityTypes();


        public static void ConfigureShadowProperties(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;

            foreach (var mutableEntityType in AllMutableEntityTypes)
            {
                ConfigureCreatedUpdatedShadowProperties(mutableEntityType);
                ConfigureIsDeletedShadowProperty(mutableEntityType);
            }
        }

        private static void ConfigureCreatedUpdatedShadowProperties(IMutableEntityType mutableEntityType)
        {
            var isTypeAuditable = IsAuditable(mutableEntityType);

            if (isTypeAuditable)
            {
                _modelBuilder.Entity(mutableEntityType.Name).Property<DateTime>("CreatedDate");
                _modelBuilder.Entity(mutableEntityType.Name).Property<DateTime>("UpdatedDate");
            }
        }

        private static void ConfigureIsDeletedShadowProperty(IMutableEntityType mutableEntityType)
        {
            _modelBuilder.Entity(mutableEntityType.Name).Property<bool>("IsDeleted");
        }


        private static bool IsAuditable(IMutableEntityType mutableEntityType)
        {
            var typeOfEntity = mutableEntityType.ClrType;
            var isTypeAuditable = AuditableService.IsTypeAuditable(typeOfEntity);

            return isTypeAuditable;
        }
    }
}
