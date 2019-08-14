using System;
using System.Collections.Generic;
using DAL.Entities;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DAL.Сontexts.Identity
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