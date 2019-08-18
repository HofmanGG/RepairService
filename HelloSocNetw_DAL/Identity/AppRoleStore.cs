using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HelloSocNetw_DAL.Identity
{
    public class AppRoleStore: RoleStore<AppUserRole, SocNetwContext, int>
    {
        public AppRoleStore(SocNetwContext dbContext, IdentityErrorDescriber identityErrorDescriber)
            : base(dbContext, identityErrorDescriber)
        { }
    }
}
