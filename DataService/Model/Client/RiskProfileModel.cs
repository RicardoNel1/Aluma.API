using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_risk_profile")]
    public class RiskProfileModel : BaseModel
    {
        public ClientModel Client { get; set; }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Goal { get; set; }

        public string LibertyRequiredRisk { get; set; }
        public string LibertyInvestmentTerm { get; set; }
        public string LibertyRiskTolerance { get; set; }
        public string LibertyRiskCapacity { get; set; }

        public string RiskAge { get; set; }
        public string RiskTerm { get; set; }
        public string RiskInflation { get; set; }
        public string RiskReaction { get; set; }
        public string RiskExample { get; set; }

        public string DerivedProfile { get; set; }
        public bool AgreeWithOutcome { get; set; }
        public string DisagreeReason { get; set; }
        public string AdvisorNotes { get; set; }
    }

    public class RiskProfileModelBuilder : IEntityTypeConfiguration<RiskProfileModel>
    {
        public void Configure(EntityTypeBuilder<RiskProfileModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            //mb.HasIndex(c => c.ClientId).IsUnique();
        }
    }
}