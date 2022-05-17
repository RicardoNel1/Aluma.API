using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("application_roa")]
    public class RecordOfAdviceModel : BaseModel
    {
        public ApplicationModel Application { get; set; }
        public AdvisorModel Advisor { get; set; }
        public ICollection<RecordOfAdviceItemsModel> SelectedProducts { get; set; }
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int AdvisorId { get; set; }
        public string BdaNumber { get; set; }
        public string Introduction { get; set; }
        public string MaterialInformation { get; set; }
        public bool Replacement_A { get; set; }
        public bool Replacement_B { get; set; }
        public bool Replacement_C { get; set; }
        public bool Replacement_D { get; set; }
        public string ReplacementReason { get; set; }
    }

    public class RecordOfAdviceModelBuilder : IEntityTypeConfiguration<RecordOfAdviceModel>
    {
        public void Configure(EntityTypeBuilder<RecordOfAdviceModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasMany(c => c.SelectedProducts)
                .WithOne(c => c.RecordOfAdvice)
                .HasForeignKey(c => c.RecordOfAdviceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}