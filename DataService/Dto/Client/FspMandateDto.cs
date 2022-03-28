using System;

namespace DataService.Dto
{
    public class FSPMandateDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        //Discretion
        public string DiscretionType { get; set; }
        public string InvestmentObjective { get; set; } 
        public string LimitedInstruction { get; set; }

        //Voting
        public string VoteInstruction { get; set; }

        //Managed Account Fees
        public string PortfolioManagementFee { get; set; }
        public string InitialFee { get; set; }
        public string AdditionalAdvisorFee { get; set; }
        public string ForeignInvestmentInitialFee { get; set; }
        public string ForeignInvestmentAnnualFee { get; set; }

        //Commisions
        public string AdminFee { get; set; }

        //Dividend
        public string DividendInstruction { get; set; }
    }
}