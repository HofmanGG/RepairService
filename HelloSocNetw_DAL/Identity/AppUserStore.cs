using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HelloSocNetw_DAL.Identity
{
    public class AppUserStore : UserStore<AppIdentityUser, AppUserRole, SocNetwContext, int>
    {
        public AppUserStore(SocNetwContext dbContext, IdentityErrorDescriber identityErrorDescriber)
            : base(dbContext, identityErrorDescriber)
        { }
    }
}