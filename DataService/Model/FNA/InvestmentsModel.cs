using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_investments")]
    public class InvestmentsModel : BaseModel
    {  
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool AttractingCGT { get; set; }
        public EstateAllocationEnum AllocateTo { get; set; }
        public double Contribution { get; set; }
        public double Escalating { get; set; }
        public string InvestmentType { get; set; }
        public double Bonds { get; set; }
        public double Equity { get; set; }
        public double Property { get; set; }
        public double OffshoreBonds { get; set; }
        public double OffshoreEquity { get; set; }
        public double OffshoreProperty { get; set; }
        public double PrivateEquity { get; set; }
        public double Cash { get; set; }
        public DataSourceEnum DataSource { get; set; }

    }

    public class InvestmentsModelBuilder : IEntityTypeConfiguration<InvestmentsModel>
    {
        public void Configure(EntityTypeBuilder<InvestmentsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }

}