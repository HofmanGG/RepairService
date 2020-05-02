using System.Runtime.Serialization;

namespace HelloSocNetw_PL.Infrastructure.Enums
{
    public class PLEnums
    {
        public enum PLRepairStatusType
        {
            Received,
            [EnumMember(Value = "In Progress")]
            InProgress,
            Done, 
            Closed
        }

        public enum PLGenderType
        {
            Male,
            Female
        }
    }
}
