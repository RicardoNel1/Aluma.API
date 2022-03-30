using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("disclosures")]
    public class DisclosureModel : BaseModel
    {
        public AdvisorModel Advisor { get; set; }
        public ClientModel Client { get; set; }
        public int Id { get; set; }
        public int AdvisorId { get; set; }
        public int ClientId { get; set; }
        public string AuthorisedUsers { get; set; }
        public bool BrokerAppointment { get; set; }
        public bool AdvisorAuthority { get; set; }
        public string AdvisorAuthorityProducts { get; set; }
    }
}