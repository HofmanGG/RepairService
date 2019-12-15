using static HelloSocNetw_PL.Infrastructure.PLEnums;

namespace HelloSocNetw_PL.Models.RepairRequestModels
{
    public class UpdateRepairRequestModel
    {
        public int RepairRequestId { get; set; }

        public string ProductName { get; set; }
        public string Comment { get; set; }

        public PLRepairStatusType RepairStatus { get; set; }
    }
}
