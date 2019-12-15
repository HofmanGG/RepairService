using System;
using System.Collections.Generic;
using System.Text;
using static HelloSocNetw_DAL.Infrastructure.DALEnums;

namespace HelloSocNetw_DAL.Entities
{
    public class RepairRequest
    {
        public int RepairRequestId { get; set; }

        public string ProductName { get; set; }
        public string Comment { get; set; }
        public DateTime RequestTime { get; set; }
        public DALRepairStatusType RepairStatus { get; set; }
        
        public string Email { get; set; }

        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
