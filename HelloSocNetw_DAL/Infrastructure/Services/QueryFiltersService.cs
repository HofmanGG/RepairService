using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelloSocNetw_DAL.Infrastructure.Services
{
    public static class QueryFiltersService
    {
        private static ModelBuilder _modelBuilder;

        private static IEnumerable<IMutableEntityType> AllMutableEntityTypes => _modelBuilder.Model.GetEntityTypes();


        public static void ConfigureQueryFilters(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;

            foreach (var entityType in AllMutableEntityTypes)
            {
                ConfigureSoftDeleteQueryFilter(entityType);
            }
        }

        private static void ConfigureSoftDeleteQueryFilter(IMutableEntityType mutableEntityType)
        {
            var parameter = Expression.Parameter(mutableEntityType.ClrType, "e");
            var body = Expression.Equal(
                Expression.Call(typeof(EF),
                    nameof(EF.Property),
                    new[] { typeof(bool) },
                    parameter,
                    Expression.Constant("IsDeleted")),
                Expression.Constant(false));

            _modelBuilder.Entity(mutableEntityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
        }
    }
}
