using Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class AppIdentityUser : IdentityUser
    {
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}