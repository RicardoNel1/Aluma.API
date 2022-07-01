using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_marital_details")]
    public class MaritalDetailsModel : BaseModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        [Required]
        public int ClientId { get; set; }
        public string MaritalStatus { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MaidenName { get; set; }
        public string IdNumber { get; set; }
        public string DateOfMarriage { get; set; }
        public bool ForeignMarriage { get; set; }
        public string CountryOfMarriage { get; set; }
        public string SpouseDateOfBirth { get; set; }
        public bool PowerOfAttorney { get; set; }


    }

}