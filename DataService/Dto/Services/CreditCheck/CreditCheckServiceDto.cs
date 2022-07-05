using Newtonsoft.Json;

namespace DataService.Dto.Services.CreditCheck
{
    public class CreditCheckAuthRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class CreditCheckAuthResponse : ApiResponseDto
    {
        public string Token { get; set; }
    }


    public class CreditCheckSettingsDto
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreditCheckSearchCriteria
    {
        [JsonProperty("identity_number")]
        public string IdNumber { get; set; }

        [JsonProperty("identity_type")]
        public string IdType { get; set; }

        [JsonProperty("consent")]
        public string Consent { get; set; }
    }
}
