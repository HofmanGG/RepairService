using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_BLL.Infrastructure
{
    public class DBOperationException: Exception
    {
        public DBOperationException()
        {
        }

        public DBOperationException(string message)
            : base(message)
        {
        }

        public DBOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
