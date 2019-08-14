using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class UserInfoModel
    {
        public int UserInfoId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }
    }
}