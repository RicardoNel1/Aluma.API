using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_summary_providing_death")]
    public class ProvidingDeathSummaryModel : BaseModel
    {
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }      
        public double TotalNeeds{ get; set; }
        public double TotalAvailable { get; set; }
    }
}