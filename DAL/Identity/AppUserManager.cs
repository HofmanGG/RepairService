using System;
using System.Collections.Generic;
using DAL.Entities;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DAL.Сontexts.Identity
{
    public class AppUserManager : UserManager<AppIdentityUser>
    {
        public AppUserManager(
            IUserStore<AppIdentityUser> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<AppIdentityUser> passwordHasher, 
            IEnumerable<IUserValidator<AppIdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<AppIdentityUser>> passwordValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services,
            ILogger<UserManager<AppIdentityUser>> logger) 
            : base(store, 
                optionsAccessor, 
                passwordHasher,
                userValidators, 
                passwordValidators,
                keyNormalizer, 
                errors,
                services, 
                logger)
        {
        }
    }
}