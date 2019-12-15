using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HelloSocNetw_PL.Infrastructure.PLEnums;

namespace HelloSocNetw_PL.Models.UserInfoModels
{
    public class NewUserInfoModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PLGenderType Gender { get; set; }

        public int CountryId { get; set; }

        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
    }
}
