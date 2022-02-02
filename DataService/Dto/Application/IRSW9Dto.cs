using System;

namespace DataService.Dto
{
    public class IRSW9Dto
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string FederalTaxClass { get; set; }
        public string OtherFederal { get; set; }
        public string LimitedTaxClass { get; set; }
        public string ExemptionCode { get; set; }
        public string FatcaCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string RequesterAddress { get; set; }
        public string AccountNumbers { get; set; }
        public string SocialSecurity { get; set; }
        public string EmployerIdNumber { get; set; }
    }
}