using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("kyc_data")]
    public class KYCDataModel : BaseModel
    {
        public ClientModel Client { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int FactoryId { get; set; }
        public string IdNumber { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public string DateOfBrith { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string DeceasedStatus { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string KycMethod { get; set; }
    }
}