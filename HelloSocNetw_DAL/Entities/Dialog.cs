using System.Collections.Generic;

namespace HelloSocNetw_DAL.Entities
{
    public class Dialog
    {
        public Dialog()
        {
            Messages = new HashSet<Message>();
        }
        public int DialogId { get; set; }

        public int FirstUserId { get; set; }
        public virtual UserInfo FirstUser { get; set; }

        public virtual int SecondUserId { get; set; }
        public virtual UserInfo SecondUser { get; set; }

        public virtual ICollection<Message> Messages { get; private set; }
    }
}
