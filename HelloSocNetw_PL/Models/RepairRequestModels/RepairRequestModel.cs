using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HelloSocNetw_PL.Infrastructure.PLEnums;

namespace HelloSocNetw_PL.Models.RepairRequestModels
{
    public class RepairRequestModel
    {
        public int RepairRequestId { get; set; }

        public string ProductName { get; set; }
        public string Comment { get; set; }

        public int RequestDay { get; set; }
        public int RequestMonth { get; set; }
        public int RequestYear { get; set; }

        public PLRepairStatusType RepairStatus { get; set; }

        public int UserInfoId { get; set; }
        public string Email { get; set; }
    }
}
