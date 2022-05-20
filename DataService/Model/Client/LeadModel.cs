using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_lead")]
    public class LeadModel : BaseModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        [Required]
        public int ClientId { get; set; }
        public AdvisorModel Advisor { get; set; }
        public int AdvisorId { get; set; }
        public int CRMId { get; set; } = 0;
        public LeadTypesEnum LeadType { get; set; }
        

    }

}