namespace DataService.Dto
{
    public class SmsPortalSettings
    {
        public string BaseUrl { get; set; }

        public string ApiToken { get; set; }
    }

    public class SmsConnectMobileSettings
    {
        public string BaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Account { get; set; }

    }
}