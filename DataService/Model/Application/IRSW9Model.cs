using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    //[Table("irsw_9")]
    public class IRSW9Model : BaseModel
    {
        public ApplicationModel Application { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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