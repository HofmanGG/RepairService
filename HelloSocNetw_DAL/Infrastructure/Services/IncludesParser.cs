using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using HelloSocNetw_DAL.Interfaces.Services;

namespace HelloSocNetw_DAL.Infrastructure.Services
{
    public class IncludesParser: IIncludesParser
    {
        public void AddIncludes<TEntity>(IQueryable<TEntity> query, string includeProperties = "") where TEntity: class
        {
            var parsedIncludes = GetParsedIncludes(includeProperties);

            foreach (var includeProperty in parsedIncludes)
            {
                query = query.Include(includeProperty);
            }
        }

        private IEnumerable<string> GetParsedIncludes(string includeProperties)
        {
            var parsedIncludes = includeProperties.Split(',' , StringSplitOptions.RemoveEmptyEntries);
            return parsedIncludes;
        }
    }
}
