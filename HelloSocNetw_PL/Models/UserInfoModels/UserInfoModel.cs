using System;
using System.Collections.Generic;
using HelloSocNetw_PL.Infrastructure.Enums;

namespace HelloSocNetw_PL.Models.UserInfoModels
{
    public class UserInfoModel
    {
        public long Id { get; set; }

        public Guid AppIdentityUserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PLEnums.PLGenderType Gender { get; set; }

        public long CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<string> Roles { get; set; }

        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }

        public string Token { get; set; }
    }
}