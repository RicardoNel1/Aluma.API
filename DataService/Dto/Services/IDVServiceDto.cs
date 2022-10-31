using Newtonsoft.Json;

namespace DataService.Dto
{
    public class IDVSettingsDto
    {
        public string BaseUrl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class IDVResponseDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string JobID { get; set; }
    }

}