using Newtonsoft.Json;

namespace DataService.Dto.Services.CreditCheck
{
    public class PBSAInitialAssessmentRequest
    {
        [JsonProperty("memberkey")]
        public string MemberKey { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("consumer_details")]
        public PBSAConsumerDetailsObject ConsumerDetails { get; set; }

    }
    public class PBSAConsumerDetailsObject
    {
        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("yourReference")]
        public string Reference { get; set; }

        [JsonProperty("searchReason")]
        public string SearchReason { get; set; }

    }
}
