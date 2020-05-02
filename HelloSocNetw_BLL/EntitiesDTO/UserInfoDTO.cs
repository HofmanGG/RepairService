using System;
using System.Collections.Generic;
using HelloSocNetw_BLL.Infrastructure.Enums;

namespace BLL.ModelsDTO
{
    public class UserInfoDTO
    {
        public long Id { get; set; }

        public Guid AppIdentityUserId { get; set; }   

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public BLLEnums.BLLGenderType Gender { get; set; }

        public long CountryId { get; set; }
        public string CountryName { get; set; }

        public string Token { get; set; }

        public ICollection<string> Roles { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}

