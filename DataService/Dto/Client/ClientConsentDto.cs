using DataService.Model;
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
        public int FinancialProviderId { get; set; }
        public int ConsentVersion { get; set; }
        public bool IsSelected { get; set; } = true;
        public DateTime Created { get; set; }
    }
}
