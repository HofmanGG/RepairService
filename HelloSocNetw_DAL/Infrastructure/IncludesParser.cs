using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloSocNetw_DAL.Infrastructure
{
    public class IncludesParser<TEntity>: IIncludesParser<TEntity> where TEntity: class
    {
        public void AddIncludes(IQueryable<TEntity> query, string includeProperties = "")
        {
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
    }
}
