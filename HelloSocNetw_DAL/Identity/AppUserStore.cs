using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace HelloSocNetw_DAL.Identity
{
    public class AppUserStore : UserStore<AppIdentityUser, 
        AppRole, 
        SocNetwContext, 
        Guid, 
        IdentityUserClaim<Guid>, 
        AppUserRole, 
        IdentityUserLogin<Guid>, 
        IdentityUserToken<Guid>, 
        IdentityRoleClaim<Guid>>
    {
        public AppUserStore(SocNetwContext dbContext, IdentityErrorDescriber identityErrorDescriber)
            : base(dbContext, identityErrorDescriber)
        { }
    }
}