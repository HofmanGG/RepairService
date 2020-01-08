using HelloSocNetw_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_NUnitTests.DALTests
{
    public class CommandTestBase : IDisposable
    {
        public CommandTestBase()
        {
            Context = SocNetwContextFactory.Create();
        }

        public SocNetwContext Context { get; }

        public void Dispose()
        {
            SocNetwContextFactory.Destroy(Context);
        }
    }
}
