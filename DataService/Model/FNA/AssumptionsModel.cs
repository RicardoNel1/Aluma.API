using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_assumptions")]
    public class AssumptionsModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public int RetirementAge { get; set; }
        public int LifeExpectancy { get; set; }
        public int YearsTillLifeExpectancy { get; set; }
        public int YearsTillRetirement { get; set; }
        public int YearsAfterRetirement { get; set; }
        public double CurrentGrossIncome { get; set; }
        public double CurrentNetIncome { get; set; }
        public InvestmentRiskEnum RetirementInvestmentRisk { get; set; }
        public InvestmentRiskEnum DeathInvestmentRisk { get; set; }
        public InvestmentRiskEnum DisabilityInvestmentRisk { get; set; }
    }

    public class AssumptionsModelBuilder : IEntityTypeConfiguration<AssumptionsModel>
    {
        public void Configure(EntityTypeBuilder<AssumptionsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.FNAId).IsUnique();

        }
    }

}