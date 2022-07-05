using Newtonsoft.Json;

namespace DataService.Dto.Services.CreditCheck
{
    public class PBSAInitialAssessmentResponse : ApiResponseDto
    {
        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("results")]
        public PBSAResultsObject[] Results { get; set; }
    }

    public class PBSAResultsObject
    {
        [JsonProperty("resultType")]
        public string ResultType { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("reasons")]
        public PBSAReasonObject[] Reasons { get; set; }
    }

    public class PBSAReasonObject
    {
        [JsonProperty("reasonCode")]
        public string ReasonCode { get; set; }

        [JsonProperty("reasonDescription")]
        public string ReasonDescription { get; set; }
    }
}
