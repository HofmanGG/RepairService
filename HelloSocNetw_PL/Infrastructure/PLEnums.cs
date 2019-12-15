using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Infrastructure
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
