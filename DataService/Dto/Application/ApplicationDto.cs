using DataService.Enum;
using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class ApplicationDto
    {
        public ClientDto Client { get; set; }
        public ICollection<ApplicationDocumentDto> Documents { get; set; }
        public int Id { get; set; }
        //public System.Nullable<Guid> AdvisorId { get; set; }
        public int ClientId { get; set; }
        public string ApplicationType { get; set; }
        public string Product { get; set; }
        public string ApplicationStatus { get; set; }

        //public bool PersonalDetailsComplete { get; set; }
        //public bool BankingDetailsComplete { get; set; }
        //public bool TaxResidencyComplete { get; set; }
        //public bool PurposeAndFundingComplete { get; set; }
        //public bool RiskProfileComplete { get; set; }
        //public bool FSPMandateComplete { get; set; }
        //public bool ConsumerProtectionComplete { get; set; }


        //public bool DocumentsCreated { get; set; }
        //public bool SignatureConsent { get; set; }
        //public DateTime SignatureConsentDate { get; set; }
        //public bool DocumentsSigned { get; set; }
        //public string BdaNumber { get; set; }
        //public bool PaymentConfirmed { get; set; }
        //public bool isActive { get; set; }
    }
}