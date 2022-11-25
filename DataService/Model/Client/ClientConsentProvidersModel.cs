using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Model.Client
{
    [Table("client_consent_provider")]
    public class ClientConsentProvidersModel : BaseModel
    {
        public ClientConsentModel ClientConsent { get; set; }
        public int Id { get; set; }
        public int ClientConsentId { get; set; }
        public int FinancialProviderId { get; set; }
    }
}
