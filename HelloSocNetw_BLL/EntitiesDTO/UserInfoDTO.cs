using System;

namespace BLL.ModelsDTO
{
    public class UserInfoDTO
    {

        public int UserInfoId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}

