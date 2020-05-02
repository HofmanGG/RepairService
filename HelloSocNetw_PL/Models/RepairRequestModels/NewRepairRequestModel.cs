using HelloSocNetw_PL.Infrastructure.Enums;

namespace HelloSocNetw_PL.Models.RepairRequestModels
{
    public class NewRepairRequestModel
    {
        public string ProductName { get; set; }
        public string Comment { get; set; }

        public PLEnums.PLRepairStatusType RepairStatus { get; set; }
    }
}
