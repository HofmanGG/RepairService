using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_BLL.Infrastructure.Exceptions
{
    public class ConflictException: Exception
    {
        public ConflictException()
            : base()
        {
        }

        public ConflictException(string message)
            : base(message)
        {
        }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ConflictException(string name, object property)
            : base($"Entity \"{name}\" with property ({property}) already exists.")
        {
        }
    }
}
