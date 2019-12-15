using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HelloSocNetw_PL.Infrastructure.PLEnums;

namespace HelloSocNetw_PL.Models
{
    public class NewRepairRequestModel
    {
        public string ProductName { get; set; }
        public string Comment { get; set; }

        public PLRepairStatusType RepairStatus { get; set; }
    }
}
