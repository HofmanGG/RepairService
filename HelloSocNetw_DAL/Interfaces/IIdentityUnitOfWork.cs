using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IIdentityUnitOfWork: IDisposable
    {
        UserManager<AppIdentityUser> UserManager { get; }
        RoleManager<AppRole> RoleManager { get; }
    }
}
