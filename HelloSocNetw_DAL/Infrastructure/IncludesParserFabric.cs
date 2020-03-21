using HelloSocNetw_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_DAL.Infrastructure
{
    public class IncludesParserFactory: IIncludesParserFactory
    {
        public IIncludesParser<TEntity> GetIncludesParser<TEntity>() where TEntity: class
        {
            return new IncludesParser<TEntity>();
        }
    }
}
