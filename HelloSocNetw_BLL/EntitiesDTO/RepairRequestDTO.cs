using System;
using HelloSocNetw_BLL.Infrastructure.Enums;

namespace HelloSocNetw_BLL.EntitiesDTO
{
    public class RepairRequestDTO
    {
        public long Id { get; set; }

        public string ProductName { get; set; }
        public string Comment { get; set; }
        public DateTime RequestTime { get; set; }
        public BLLEnums.BLLRepairStatusType RepairStatus { get; set; }

        public long UserInfoId { get; set; }
        public string Email { get; set; }
    }
}
