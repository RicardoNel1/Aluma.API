using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_providing_on_disability")]
    public class ProvidingOnDisabilityModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double ShortTermProtection { get; set; }
        public int IncomeProtectionTerm_Months { get; set; }
        public double ShortTermEscalation { get; set; }
        public double LongTermProtection { get; set; }
        public double LongTermEscalation { get; set; }
        public double IncomeNeeds { get; set; }
        public int NeedsTerm_Years { get; set; }
        public double IncomeNeedsEscalation { get; set; }
        public double LiabilitiesToClear { get; set; }
        public double CapitalNeeds { get; set; }
    }

    public class ProvidingOnDisabilityModelBuilder : IEntityTypeConfiguration<ProvidingOnDisabilityModel>
    {
        public void Configure(EntityTypeBuilder<ProvidingOnDisabilityModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.FNAId).IsUnique();
           
        }
    }

}