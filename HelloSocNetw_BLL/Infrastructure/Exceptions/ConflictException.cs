using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_BLL.Infrastructure.Exceptions
{
    public class ConflictException: Exception
    {
        public ConflictException(string name, object property)
            : base($"Entity \"{name}\" with propety ({property}) already exists.")
        {
        }
    }
}
