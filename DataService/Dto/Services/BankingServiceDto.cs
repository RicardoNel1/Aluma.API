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

        public string Reference { get; set; }

        public string AccountType { get; set; }

        public string VerificationType { get; set; }

        public string BranchCode { get; set; }

        public string AccountNumber { get; set; }

        public string IdNumber { get; set; }

        public string Initials { get; set; }

        public string InitialsMatch { get; set; }

        public string Surname { get; set; }

        public string FoundAtBank { get; set; }

        public string AccOpen { get; set; }

        public string OlderThan3Months { get; set; }

        public string TypeCorrect { get; set; }

        public string IdNumberMatch { get; set; }

        public string SurnameMatch { get; set; }

        public string AcceptDebits { get; set; }

        public string AcceptCredits { get; set; }

        public string BankName { get; set; }

    }
}