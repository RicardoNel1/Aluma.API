using Newtonsoft.Json;

namespace DataService.Dto
{
    public class SettingsDto
    {
        public string BaseUrl { get; set; }
        public string Authorization { get; set; }
        public string Memberkey { get; set; }
        public string Password { get; set; }
    }

    public class BankValidationResponseDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string JobID { get; set; }
    }

    public class XdsbvsDto
    {
        public string JobStatus { get; set; }
        public string JobID { get; set; }
    }

    public class VerificationStatusResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public BankDetailsDto AVS { get; set; }
    }

    public class BankDetailsDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        [JsonProperty("userReference")]
        public string Reference { get; set; }

        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        public string VerificationType { get; set; }

        [JsonProperty("branchCode")]
        public string BranchCode { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("initialMatch")]
        public string InitialsMatch { get; set; }

        [JsonProperty("lastName")]
        public string Surname { get; set; }

        [JsonProperty("accountExists")]
        public string FoundAtBank { get; set; }

        [JsonProperty("accountOpen")]
        public string AccOpen { get; set; }

        [JsonProperty("accountOpenGtThreeMonths")]
        public string OlderThan3Months { get; set; }

        [JsonProperty("accountTypeValid")]
        public string TypeCorrect { get; set; }

        [JsonProperty("accountIdMatch")]
        public string IdNumberMatch { get; set; }

        [JsonProperty("lastNameMatch")]
        public string SurnameMatch { get; set; }

        [JsonProperty("accountAcceptsDebits")]
        public string AcceptDebits { get; set; }

        [JsonProperty("accountAcceptsCredits")]
        public string AcceptCredits { get; set; }

        public string BankName { get; set; }

    }
}