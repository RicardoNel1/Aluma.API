using DataService.Model;
using DataService.Model.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.Client
{
    public class ClientConsentDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool OtpVerified { get; set; }
        public List<ClientConsentProviderDto> ConsentedProviders { get; set; }
    }
}
