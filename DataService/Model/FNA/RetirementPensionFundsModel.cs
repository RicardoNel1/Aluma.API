using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_retirement_pension_funds")]
    public class RetirementPensionFundsModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double MonthlyContributions { get; set; }
        public double EscPercent { get; set; }
        public double Growth { get; set; }
        public DataSourceEnum DataSource { get; set; }

    }

    public class RetirementPensionFundsModelBuilder : IEntityTypeConfiguration<RetirementPensionFundsModel>
    {
        public void Configure(EntityTypeBuilder<RetirementPensionFundsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }

}