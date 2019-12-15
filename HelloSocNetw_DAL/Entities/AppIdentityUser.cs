using System;
using System.Collections.Generic;
using HelloSocNetw_DAL.Identity;
using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.Entities
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}