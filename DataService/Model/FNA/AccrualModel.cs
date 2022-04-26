using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("accrual")]
    public class AccrualModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public double ClientAssetsCommencement { get; set; }
        public double SpouseAssetsCommencement { get; set; }
        public double ClientEstateCurrent { get; set; }
        public double SpouseEstateCurrent { get; set; }
        public double ClientLiabilities { get; set; }
        public double SpouseLiabilities { get; set; }
        public double ClientExcludedValue { get; set; }
        public double SpouseExcludedValue { get; set; }
        public double Cpi { get; set; }
        public double Offset { get; set; }
    }

    public class AccrualModelBuilder : IEntityTypeConfiguration<AccrualModel>
    {
        public void Configure(EntityTypeBuilder<AccrualModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
