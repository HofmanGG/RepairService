using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_DAL.Infrastructure.Exceptions
{
    public class ObjectNotFoundException: Exception
    {
        public ObjectNotFoundException()
        {
        }

        public ObjectNotFoundException(string message)
            : base(message)
        {
        }

        public ObjectNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
