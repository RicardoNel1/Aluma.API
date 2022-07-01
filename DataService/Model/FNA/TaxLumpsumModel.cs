using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_tax_lumpsum")]
    public class TaxLumpsumModel : BaseModel
    {
        public ClientFNAModel FNA { get; set; }

        public int Id { get; set; }
        public int FnaId { get; set; }
        public double PreviouslyDisallowed { get; set; }
        public double RetirementReceived { get; set; }
        public double WithdrawalReceived { get; set; }
        public double SeverenceReceived { get; set; }
        public double TaxPayable { get; set; }

    }


    public class TaxLumpsumBuilder : IEntityTypeConfiguration<TaxLumpsumModel>
    {
        public void Configure(EntityTypeBuilder<TaxLumpsumModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //builder.HasIndex(c => c.FnaId).IsUnique();
        }
    }
}
