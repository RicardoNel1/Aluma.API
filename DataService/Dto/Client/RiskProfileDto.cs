using System;

namespace DataService.Dto
{
    public class RiskProfileDto
    {
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
}