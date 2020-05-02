namespace HelloSocNetw_BLL.Entities
{
    public class EmailSettings
    {
        public string FromName { get; set; }
        public string ToName { get; set; }

        public string MailHost { get; set; }
        public int MailPort { get; set; }

        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }

        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
