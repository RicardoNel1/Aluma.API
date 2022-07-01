using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_capital_gains_tax")]
    public class CapitalGainsTaxModel : BaseModel
    {
        public ClientFNAModel FNA { get; set; }
        public int Id { get; set; }
        public int FnaId { get; set; }
        public double PreviousCapitalLosses { get; set; }
        public double TotalCGTPayable { get; set; }
    }

    public class CapitalGainsTaxModelBuilder : IEntityTypeConfiguration<CapitalGainsTaxModel>
    {
        public void Configure(EntityTypeBuilder<CapitalGainsTaxModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }
}
