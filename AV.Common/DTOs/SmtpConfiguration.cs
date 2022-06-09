namespace AV.Common.DTOs
{
    public class SmtpConfiguration
    {
        public string SmtpMailServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public int AlternativePort { get; set; }
        public bool UseTLS { get; set; }
        public string FromEmailAddress { get; set; }
    }
}
