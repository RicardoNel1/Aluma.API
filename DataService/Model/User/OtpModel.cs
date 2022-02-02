using DataService.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("otp")]
    public class OtpModel : BaseModel
    {
        public UserModel User { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public OtpTypesEnum OtpType { get; set; }
        public string Otp { get; set; }
        public int ApplicationId { get; set; }
        public bool isValidated { get; set; }
        public bool isExpired { get; set; }
    }
}