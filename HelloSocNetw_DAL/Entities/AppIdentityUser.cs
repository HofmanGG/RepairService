using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.Entities
{
    public class AppIdentityUser : IdentityUser
    {
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}