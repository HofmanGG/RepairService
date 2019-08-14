using Entities;

namespace DAL.Entities
{
    public class SubscribersTable
    {
        public int UserId { get; set; }
        public virtual UserInfo User { get; set; }

        public int SubscriberId { get; set; }
        public virtual UserInfo Subscriber { get; set; }
    }
}