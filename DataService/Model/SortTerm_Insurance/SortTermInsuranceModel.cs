using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("ShortTermInsurance")]
    public class SortTermInsuranceModel : BaseModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public int ClientId { get; set; }
        public string Provider { get; set; }
        public string Type { get; set; }
        public string PolicyNumber { get; set; }
        public double MonthlyPremium { get; set; }
    }

    public class SortTermInsuranceModelBuilder : IEntityTypeConfiguration<SortTermInsuranceModel>
    {
        public void Configure(EntityTypeBuilder<SortTermInsuranceModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}