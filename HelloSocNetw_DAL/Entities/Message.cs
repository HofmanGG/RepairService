﻿namespace HelloSocNetw_DAL.Entities
{
    public class Message
    {
        public int MessageId { get; set; }

        public int WriterId { get; set; }
        public virtual UserInfo Writer { get; set; }

        public string Text { get; set; }
    }
}
