using System.Collections.Generic;
using Entities;

namespace DAL.Entities
{
    public class Dialog
    {
        public Dialog()
        {
            Messages = new HashSet<Message>();
        }

        public int FirstUserId { get; set; }
        public UserInfo UserInfo { get; set; }

        public int SecondUserId { get; set; }
        public UserInfo SecondUser { get; set; }

        public ICollection<Message> Messages { get; private set; }
    }
}
