using System;

namespace DataService.Dto
{
    public class RiskProfileDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        //public string Goal { get; set; }

        //public string LibertyRequiredRisk { get; set; }
        //public string LibertyInvestmentTerm { get; set; }
        //public string LibertyRiskTolerance { get; set; }
        //public string LibertyRiskCapacity { get; set; }

        public int RiskAge { get; set; }
        public int RiskTerm { get; set; }
        public int RiskInflation { get; set; }
        public int RiskReaction { get; set; }
        public int RiskExample { get; set; }

        public string DerivedProfile { get; set; }
        public bool AgreeWithOutcome { get; set; }
        public string DisagreeReason { get; set; }
        public string AdvisorNotes { get; set; }
    }
}