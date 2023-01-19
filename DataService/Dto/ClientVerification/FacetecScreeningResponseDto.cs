using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.ClientVerification
{
    public class FacetecScreeningResponsesDto
    {
        public string Status { get; set; }
        [JsonProperty("Error")]
        public string ErrorMessage { get; set; }

        public string Message { get; set; }
        public FacetecScreeningRequestDto Result { get; set; }
    }
}
