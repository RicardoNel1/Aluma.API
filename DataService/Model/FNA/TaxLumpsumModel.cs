using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("tax_lumpsum")]
    public class TaxLumpsumModel : BaseModel
    {
        public int Id { get; set; }
        public int FnaId { get; set; }
        public float PreviouslyDisallowed { get; set; }
        public float RetirementReceived { get; set; }
        public float WithdrawalReceived { get; set; }
        public float SeverenceReceived { get; set; }
        public float TaxPayable { get; set; }

    }


    public class TaxLumpsumBuilder : IEntityTypeConfiguration<TaxLumpsumModel>
    {
        public void Configure(EntityTypeBuilder<TaxLumpsumModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
