using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.ClientVerification
{
    public class AMLScreeningSubmitResponseDto
    {
        public string Status { get; set; }
        [JsonProperty("Error")]
        public string ErrorMessage { get; set; }

        public string Message { get; set; }
        public AMLSubmitResponseResultDto Result { get; set; }
    }

    public class AMLSubmitResponseResultDto
    {
        public AMLSubmitResponseConsumerDto ConsumerDetails { get; set; }
    }

    public class AMLSubmitResponseConsumerDto
    {
        public int ConsumerID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public string PassportNumber { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string XML { get; set; }
        public string TempReference { get; set; }
        public string EnquiryID { get; set; }
        public string EnquiryResultID { get; set; }
        public string Reference { get; set; }
    }
}
