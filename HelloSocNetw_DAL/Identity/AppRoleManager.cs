using System.Collections.Generic;
using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HelloSocNetw_DAL.Identity
{
    public class AppRoleManager : RoleManager<AppIdentityUser>
    {
        public AppRoleManager(
            IRoleStore<AppIdentityUser> store,
            IEnumerable<IRoleValidator<AppIdentityUser>> roleValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors,
            ILogger<RoleManager<AppIdentityUser>> logger)
            : base(store,
                roleValidators,
                keyNormalizer,
                errors,
                logger)
        {
        }
    }
}