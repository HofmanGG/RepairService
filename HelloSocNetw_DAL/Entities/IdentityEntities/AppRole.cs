using HelloSocNetw_DAL.Infrastructure.Attributes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HelloSocNetw_DAL.Entities.IdentityEntities
{
    [Auditable]
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}