using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.ClientVerification
{
    public class AMLScreeningResultResponseDto
    {
        public string Status { get; set; }
        [JsonProperty("Error")]
        public string ErrorMessage { get; set; }

        public string Message { get; set; }
        public List<AMLResultResponseResultDto> Result { get; set; }
    }

    public class AMLResultResponseResultDto
    {
        public string ID { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }
        public string EntityUniqueID { get; set; }
        public string ResultDate { get; set; }
        public string DateListed { get; set; }
        public string Gender { get; set; }
        public string BestNameScore { get; set; }
        public string ReasonListed { get; set; }
        public string ListReferenceNumber { get; set; }
        public string Comments { get; set; }
        public List<AMLResultResponseAdditionalInfoDto> AdditionalInfo { get; set; }
    }

    public class AMLResultResponseAdditionalInfoDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public object Comment { get; set; }
    }
}
