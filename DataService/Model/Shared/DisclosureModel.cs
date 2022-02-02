using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("disclosures")]
    public class DisclosureModel : DocumentBaseModel
    {
        public AdvisorModel Advisor { get; set; }
        public ClientModel Client { get; set; }
        public UserDocumentModel Document { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AdvisorId { get; set; }
        public int ClientId { get; set; }
        public int DocumentId { get; set; }

        //CPA Options
        public bool AlumaOffers { get; set; }

        public bool OtherOffers { get; set; }
        public bool ReputableOrg { get; set; }
        public bool OtherMethodOfCommunication { get; set; }
        public string MethodOfCommunication { get; set; }

        public string AuthorisedUsers { get; set; }

        public bool BrokerAppointment { get; set; }
        public bool AdvisorAuthority { get; set; }
        public string AdvisorAuthorityProducts { get; set; }
    }
}