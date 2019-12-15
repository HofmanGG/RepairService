using System;
using System.Collections.Generic;
using static HelloSocNetw_PL.Infrastructure.PLEnums;

namespace HelloSocNetw_PL.Models
{
    public class UserInfoModel
    {
        public int UserInfoId { get; set; }

        public Guid AppIdentityUserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PLGenderType Gender { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<string> Roles { get; set; }

        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }

        public string Token { get; set; }
    }
}