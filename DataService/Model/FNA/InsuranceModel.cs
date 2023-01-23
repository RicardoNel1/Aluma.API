using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_insurance")]
    public class InsuranceModel : BaseModel
    {
        //public ClientModel Client { get; set; }
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public string Beneficiary { get; set; }
        public double LifeCover { get; set; }
        public double Disability { get; set; }
        public double DreadDisease { get; set; }
        public double AbsoluteIpPm { get; set; }
        public double ExtendedIpPm { get; set; }
        public EstateAllocationEnum AllocateTo { get; set; }
    }

    public class InsuranceModelBuilder : IEntityTypeConfiguration<InsuranceModel>
    {
        public void Configure(EntityTypeBuilder<InsuranceModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
