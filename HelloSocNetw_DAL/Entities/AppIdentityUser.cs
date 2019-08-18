using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.Entities
{
    public class AppIdentityUser : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}