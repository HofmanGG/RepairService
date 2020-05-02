using Microsoft.AspNetCore.Identity;
using System;

namespace HelloSocNetw_DAL.Entities.IdentityEntities
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppIdentityUser User { get; set; }
        public AppRole Role { get; set; }
    }
}
