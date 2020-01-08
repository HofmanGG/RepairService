using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Infrastructure.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
