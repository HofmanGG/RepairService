using HelloSocNetw_DAL.Interfaces;

namespace HelloSocNetw_DAL.Infrastructure
{
    public interface IIncludesParserFactory
    {
        IIncludesParser<TEntity> GetIncludesParser<TEntity>() where TEntity : class;
    }
}