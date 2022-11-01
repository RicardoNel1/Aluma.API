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
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseObject
    {
        public string Token { get; set; }
    }

    public class IDVRequestDto
    {
        public string IdNumber { get; set; }
        public string YourReference { get; set; }
    }

    public class IDVRealTimeResponseDto
    {
        public string Status { get; set; }

        public RealtimeResult RealTimeResult { get; set; }
        //public string Message { get; set; }
        //public string Error { get; set; }
        //public string JobID { get; set; }
    }

    public class RealtimeResult
    {
        [JsonProperty("traceId")]
        public int TraceId { get; set; }
        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }
        [JsonProperty("haIdno")]
        public string HomeAffairsIdNo { get; set; }
        [JsonProperty("idnoMatchStatus")]
        public string IdNoMatchStatus { get; set; }
        [JsonProperty("iDBookIssuedDate")]
        public string IdBookIssuedDate { get; set; }
        [JsonProperty("identityDocumentType")]
        public string IdType { get; set; }
        [JsonProperty("idCardInd")]
        public string IdCardInd { get; set; }
        [JsonProperty("idCardDate")]
        public string IdCardDate { get; set; }
        [JsonProperty("idBlocked")]
        public string IdBlocked { get; set; }
        [JsonProperty("firstNames")]
        public string FirstName { get; set; }
        [JsonProperty("surName")]
        public string Surname { get; set; }
        [JsonProperty("dob")]
        public string DateOfBirth { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("citizenship")]
        public string Citizenship { get; set; }
        [JsonProperty("countryofBirth")]
        public string CountryofBirth { get; set; }
        [JsonProperty("deceasedStatus")]
        public string DeceasedStatus { get; set; }
        [JsonProperty("deceasedDate")]
        public string DeceasedDate { get; set; }
        [JsonProperty("deathPlace")]
        public string DeathPlace { get; set; }
        [JsonProperty("causeOfDeath")]
        public string CauseOfDeath { get; set; }
        [JsonProperty("maritalStatus")]
        public string MaritalStatus { get; set; }
        [JsonProperty("marriageDate")]
        public string MarriageDate { get; set; }
    }

}