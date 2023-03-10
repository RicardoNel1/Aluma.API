using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_retirement_preservation_funds")]
    public class RetirementPreservationFundsModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double Growth { get; set; }

    }

    public class RetirementPreservationFundsModelBuilder : IEntityTypeConfiguration<RetirementPreservationFundsModel>
    {
        public void Configure(EntityTypeBuilder<RetirementPreservationFundsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }

}