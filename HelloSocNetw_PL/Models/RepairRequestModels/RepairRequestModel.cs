using HelloSocNetw_PL.Infrastructure.Enums;

namespace HelloSocNetw_PL.Models.RepairRequestModels
{
    public class RepairRequestModel
    {
        public long Id { get; set; }

        public string ProductName { get; set; }
        public string Comment { get; set; }

        public int RequestDay { get; set; }
        public int RequestMonth { get; set; }
        public int RequestYear { get; set; }

        public PLEnums.PLRepairStatusType RepairStatus { get; set; }

        public long UserInfoId { get; set; }
        public string Email { get; set; }
    }
}
