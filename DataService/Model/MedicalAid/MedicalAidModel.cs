
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("medical_aid")]
    public class MedicalAidModel : BaseModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public int ClientId { get; set; }
        public string Provider { get; set; }
        public string Type { get; set; }
        public string MedicalAidNumber { get; set; }
        public bool MainMember { get; set; } = true;
        public bool NetworkPlan { get; set; } = true;
        public bool SavingsPlan { get; set; } = true;
        public bool GapCover { get; set; } = true;
        public int NumberOfDependants { get; set; }
        public double MonthlyPremium { get; set; }
        public double MaxAnnualSavings { get; set; }
    }

    public class MedicalAidModelBuilder : IEntityTypeConfiguration<MedicalAidModel>
    {
        public void Configure(EntityTypeBuilder<MedicalAidModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}