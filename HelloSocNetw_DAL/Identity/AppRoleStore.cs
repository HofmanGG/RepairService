using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace HelloSocNetw_DAL.Identity
{
    public class AppRoleStore: RoleStore<AppRole, SocNetwContext, Guid, AppUserRole, IdentityRoleClaim<Guid>>
    {
        public AppRoleStore(SocNetwContext dbContext, IdentityErrorDescriber identityErrorDescriber)
            : base(dbContext, identityErrorDescriber)
        { }
    }
}
