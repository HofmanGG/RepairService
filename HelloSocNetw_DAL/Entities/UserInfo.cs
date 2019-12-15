
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HelloSocNetw_DAL.Infrastructure.DALEnums;

namespace HelloSocNetw_DAL.Entities
{
    public class UserInfo
    {
        public UserInfo()
        {
            RepairRequests = new HashSet<RepairRequest>();
        }

        public int UserInfoId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DALGenderType Gender { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<RepairRequest> RepairRequests { get; set; }

        public Guid AppIdentityUserId { get; set; }
        public virtual AppIdentityUser AppIdentityUser { get; set; }
    }
}