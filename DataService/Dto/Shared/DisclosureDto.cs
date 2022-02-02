using System;

namespace DataService.Dto
{
    public class DisclosureDto
    {
        public UserDocumentDto Document { get; set; }
        public ClientDto Client { get; set; }
        public AdvisorDto Advisor { get; set; }
        public int Id { get; set; }
        public int ClientId { get; }
        public int AdvisorId { get; set; }
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