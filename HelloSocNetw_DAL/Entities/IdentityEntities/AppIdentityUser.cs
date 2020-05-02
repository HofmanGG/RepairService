using System;
using System.Collections.Generic;
using HelloSocNetw_DAL.Infrastructure.Attributes;
using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.Entities.IdentityEntities
{
    [Auditable]
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public long UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}