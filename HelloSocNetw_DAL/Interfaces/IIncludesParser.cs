using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IIncludesParser<TEntity> where TEntity: class
    {
        void AddIncludes(IQueryable<TEntity> query, string includeProperties = "");
    }
}
