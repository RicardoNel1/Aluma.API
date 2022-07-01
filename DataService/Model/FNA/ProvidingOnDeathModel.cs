using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_providing_on_death")]
    public class ProvidingOnDeathModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int FNAId { get; set; }
        public ClientFNAModel FNA { get; set; }
        public double IncomeNeeds { get; set; }
        public int IncomeTerm_Years { get; set; }
        public double CapitalNeeds { get; set; }
        public string Available_InsuranceDescription { get; set; }
        public double Available_Insurance_Amount { get; set; }
        public double RetirementFunds { get; set; }
        public double Available_PreTaxIncome_Amount { get; set; }
        public int Available_PreTaxIncome_Term { get; set; }
    }

    public class ProvidingOnDeathModelBuilder : IEntityTypeConfiguration<ProvidingOnDeathModel>
    {
        public void Configure(EntityTypeBuilder<ProvidingOnDeathModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.FNAId).IsUnique();
           
        }
    }

}