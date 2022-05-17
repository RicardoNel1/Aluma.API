using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_employment_details")]
    public class EmploymentDetailsModel : BaseModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        [Required]
        public int ClientId { get; set; }
        public string EmploymentStatus { get; set; }
        public string Employer { get; set; }
        public string Industry { get; set; }
        public string Occupation { get; set; }
        public string WorkNumber { get; set; }


    }
}