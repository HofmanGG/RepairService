using HelloSocNetw_DAL.Infrastructure.Attributes;
using HelloSocNetw_DAL.Interfaces;
using System;
using HelloSocNetw_DAL.Infrastructure.Enums;

namespace HelloSocNetw_DAL.Entities
{
    [Auditable]
    public class RepairRequest: IEntity
    {
        public long Id { get; set; }

        public string ProductName { get; set; }
        public string Comment { get; set; }
        public DateTime RequestTime { get; set; }
        public DALEnums.DALRepairStatusType RepairStatus { get; set; }

        public long UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
