using System;
using System.Collections.Generic;
using static HelloSocNetw_BLL.Infrastructure.BLLEnums;

namespace BLL.ModelsDTO
{
    public class UserInfoDTO
    {
        public int UserInfoId { get; set; }

        public Guid AppIdentityUserId { get; set; }   

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public BLLGenderType Gender { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public string Token { get; set; }

        public ICollection<string> Roles { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}

