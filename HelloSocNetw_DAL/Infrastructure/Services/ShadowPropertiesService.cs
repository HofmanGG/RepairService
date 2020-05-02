using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HelloSocNetw_DAL.Infrastructure.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelloSocNetw_DAL.Infrastructure.Services
{
    public static class ShadowPropertiesService
    {
        public static void ConfigureShadowProperties(ModelBuilder modelBuilder)
        {
            ConfigureShadowPropertiesService.ConfigureShadowProperties(modelBuilder);
        }

        public static void SetShadowPropertiesValues(IEnumerable<EntityEntry> entries)
        {
            SetShadowPropertiesService.SetShadowPropertiesValues(entries);
        }
    }
}
