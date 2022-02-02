using DataService.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("address")]
    public class AddressModel : BaseModel
    {
        public UserModel User { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UnitNumber { get; set; }
        public string Complex { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public AddressTypesEnum Type { get; set; }

        public bool InCareAddress { get; set; }
        public string InCareName { get; set; }
        public int YearsAtAddress { get; set; }
        public string AddressSameAs { get; set; }
    }
}