using HelloSocNetw_DAL.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HelloSocNetw_DAL.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}