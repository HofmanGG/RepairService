using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using HelloSocNetw_DAL.Entities.IdentityEntities;

namespace HelloSocNetw_DAL.Identity
{
    public class AppRoleStore: RoleStore<AppRole, SocNetwContext, Guid, AppUserRole, IdentityRoleClaim<Guid>>
    {
        public AppRoleStore(SocNetwContext dbContext, IdentityErrorDescriber identityErrorDescriber)
            : base(dbContext, identityErrorDescriber)
        { }
    }
}
