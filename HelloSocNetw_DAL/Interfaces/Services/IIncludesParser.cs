using System.Linq;

namespace HelloSocNetw_DAL.Interfaces.Services
{
    public interface IIncludesParser
    {
        void AddIncludes<TEntity>(IQueryable<TEntity> query, string includeProperties = "") where TEntity: class;
    }
}
