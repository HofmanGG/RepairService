using System.Collections.Generic;
using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HelloSocNetw_DAL.Identity
{
    public class AppRoleManager : RoleManager<AppUserRole>
    {
        public AppRoleManager(
            IRoleStore<AppUserRole> store,
            IEnumerable<IRoleValidator<AppUserRole>> roleValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors,
            ILogger<RoleManager<AppUserRole>> logger)
            : base(store,
                roleValidators,
                keyNormalizer,
                errors,
                logger)
        {
        }
    }
}