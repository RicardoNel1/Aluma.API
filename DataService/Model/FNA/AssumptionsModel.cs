using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("assumptions")]
    public class AssumptionsModel : BaseModel
    {
        public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RetirementAge { get; set; }
        public double CurrentNetIncome { get; set; }
        public InvestmentRiskEnum DeathInvestmentRisk { get; set; }
        public InvestmentRiskEnum DisabilityInvestmentRisk { get; set; }
    }

    public class AssumptionsModelBuilder : IEntityTypeConfiguration<AssumptionsModel>
    {
        public void Configure(EntityTypeBuilder<AssumptionsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
           
        }
    }

}