using System;
using HelloSocNetw_PL.Infrastructure.Enums;

namespace HelloSocNetw_PL.Models.UserInfoModels
{
    public class UpdateUserInfoModel
    {
        public long Id { get; set; }

        public Guid AppIdentityUserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PLEnums.PLGenderType Gender { get; set; }

        public long CountryId { get; set; }

        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
    }
}
