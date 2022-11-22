using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.Client
{
    public class ClientConsentProviderDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientConsentId { get; set; }
        public DateTime Created { get; set; }
        public int FinancialProviderId { get; set; }
    }
}
