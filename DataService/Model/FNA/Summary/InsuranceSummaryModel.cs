using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_summary_insurance")]
    public class InsuranceSummaryModel : BaseModel
    {
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double TotalToSpouse { get; set; }
        public double TotalToThirdParty { get; set; }
        public double TotalToLiquidity{ get; set; }
    }
}