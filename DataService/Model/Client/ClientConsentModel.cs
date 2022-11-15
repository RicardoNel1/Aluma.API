using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Model.Client
{
    [Table("client_consent_provider")]
    public class ClientConsentModel : BaseModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int FinancialProviderId { get; set; }
        [NotMapped]
        public bool isSelected { get; set; }
    }
}
