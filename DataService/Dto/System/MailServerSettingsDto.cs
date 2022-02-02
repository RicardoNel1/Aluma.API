namespace DataService.Dto
{
    public class MailServerSettingsDto
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string SMTPServer { get; set; }
        public string Username { get; set; }
        public int Port { get; set; }
        public string TemplateFolder { get; set; }
    }
}