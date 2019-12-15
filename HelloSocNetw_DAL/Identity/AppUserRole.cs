using HelloSocNetw_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_DAL.Identity
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public virtual AppIdentityUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
