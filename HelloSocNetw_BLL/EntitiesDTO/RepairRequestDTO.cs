using System;
using System.Collections.Generic;
using System.Text;
using static HelloSocNetw_BLL.Infrastructure.BLLEnums;

namespace HelloSocNetw_BLL.EntitiesDTO
{
    public class RepairRequestDTO
    {
        public int RepairRequestId { get; set; }

        public string ProductName { get; set; }
        public string Comment { get; set; }
        public DateTime RequestTime { get; set; }
        public BLLRepairStatusType RepairStatus { get; set; }

        public int UserInfoId { get; set; }
        public string Email { get; set; }
    }
}
