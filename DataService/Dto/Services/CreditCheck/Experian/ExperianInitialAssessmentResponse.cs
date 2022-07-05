
using Newtonsoft.Json;

namespace DataService.Dto.Services.CreditCheck
{
    public class ExperianInitialAssessmentResponse : ApiResponseDto
    {
        [JsonProperty("response_status")]
        public string ResponseStatus { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("return_data")]
        public ExperianReturnObject ReturnData { get; set; }
    }

    public class ExperianReturnObject
    {
        [JsonProperty("transaction_id")]
        public string TransactionID { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("search_criteria")]
        public ExperianSearchObject SearchCriteria { get; set; }

        [JsonProperty("fas")]
        public ExperianFASObject FAS { get; set; }

        [JsonProperty("client_custom_scores")]
        public ExperianClientCustomScoreObject[] ClientCustomScores { get; set; }

        [JsonProperty("pin_points")]
        public ExperianPinPointObject[] PinPoints { get; set; }

        [JsonProperty("client_generic_scores")]
        public ExperianClientGenericScoreObject[] ClientGenericScores { get; set; }

    }

    public class ExperianSearchObject
    {
        [JsonProperty("identity_number")]
        public string IdNumber { get; set; }

        [JsonProperty("identity_type")]
        public string IdType { get; set; }

        [JsonProperty("consent")]
        public string Consent { get; set; }
    }

    public class ExperianFASObject
    {
        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ExperianPinPointObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ExperianBaseScoreObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("decline_r_1")]
        public string DeclineReason1 { get; set; }

        [JsonProperty("decline_r_2")]
        public string DeclineReason2 { get; set; }

        [JsonProperty("decline_r_3")]
        public string DeclineReason3 { get; set; }

        [JsonProperty("decline_r_4")]
        public string DeclineReason4 { get; set; }

        [JsonProperty("decline_r_5")]
        public string DeclineReason5 { get; set; }

        [JsonProperty("thin_file_indicator")]
        public string ThinFileIndicator { get; set; }
    }

    public class ExperianClientGenericScoreObject : ExperianBaseScoreObject
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ExperianClientCustomScoreObject : ExperianBaseScoreObject
    {
        [JsonProperty("risk_grade")]
        public string RiskGrade { get; set; }
    }
}
