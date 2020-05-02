using System;
using System.Collections.Generic;
using System.Text;
using HelloSocNetw_DAL.Infrastructure.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelloSocNetw_DAL.Infrastructure.Services
{
    public static class SetShadowPropertiesService
    {
        public static void SetShadowPropertiesValues(IEnumerable<EntityEntry> entries)
        {
            foreach (var entityEntry in entries)
            {
                SetCreatedUpdatedShadowProperties(entityEntry);
                SetIsDeletedShadowProperty(entityEntry);
            }
        }

        private static void SetCreatedUpdatedShadowProperties(EntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
            {
                var isTypeAuditable = IsAuditable(entityEntry);

                if (isTypeAuditable)
                {
                    entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    }
                }
            }
        }

        private static void SetIsDeletedShadowProperty(EntityEntry entityEntry)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Property("IsDeleted").CurrentValue = false;
                    break;
                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Property("IsDeleted").CurrentValue = true;
                    break;
            }
        }


        private static bool IsAuditable(EntityEntry entityEntry)
        {
            var typeOfEntity = entityEntry.Entity.GetType();
            var typeIsAuditable = AuditableService.IsTypeAuditable(typeOfEntity);

            return typeIsAuditable;
        }
    }
}
