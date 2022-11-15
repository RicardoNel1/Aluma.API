using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.Client
{
    public class ClientConsentDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int FinancialProviderId { get; set; }
        public bool IsSelected { get; set; }
    }
}
