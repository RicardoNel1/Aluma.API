using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class ApplicationDto
    {
        public ClientDto Client { get; set; }
        public ICollection<ApplicationDocumentDto> Documents { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ApplicationType { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ApplicationStatus { get; set; }
        public bool showRiskMismatch { get; set; }
        public bool showRecordOfAdvice { get; set; }
        public bool DocumentsCreated { get; set; }
        public bool DocumentsSigned { get; set; }

        public bool SignatureConsent { get; set; }
        public DateTime SignatureConsentDate { get; set; }

    }
}