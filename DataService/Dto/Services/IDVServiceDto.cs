using Newtonsoft.Json;

namespace DataService.Dto
{
    public class IDVSettingsDto
    {
        public string BaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationDto
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class AuthResponseObject
    {
        public string Token { get; set; }
    }

    public class IDVResponseDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string JobID { get; set; }
    }

}