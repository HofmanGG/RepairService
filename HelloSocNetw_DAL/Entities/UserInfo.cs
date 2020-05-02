using HelloSocNetw_DAL.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using HelloSocNetw_DAL.Entities.IdentityEntities;
using HelloSocNetw_DAL.Infrastructure.Enums;
using HelloSocNetw_DAL.Interfaces;

namespace HelloSocNetw_DAL.Entities
{
    [Auditable]
    public class UserInfo: IEntity
    {
        public UserInfo()
        {
            RepairRequests = new List<RepairRequest>();
        }

        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DALEnums.DALGenderType Gender { get; set; }

        public long CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<RepairRequest> RepairRequests { get; set; }

        public Guid AppIdentityUserId { get; set; }
        public AppIdentityUser AppIdentityUser { get; set; }
    }
}