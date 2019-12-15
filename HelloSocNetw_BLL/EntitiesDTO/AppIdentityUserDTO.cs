using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_BLL.EntitiesDTO
{
    public class AppIdentityUserDTO
    {
        public string Id { get; set; }

        public int UserInfoId { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public string NormalizedEmail { get; set; }
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
