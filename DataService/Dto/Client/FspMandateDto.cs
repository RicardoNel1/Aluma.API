namespace DataService.Dto
{
    public class FSPMandateDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        //Discretion
        public string DiscretionType { get; set; } = "full";
        public string InvestmentObjective { get; set; } = "capitalGrowth";
        public string LimitedInstruction { get; set; }

        //Voting
        public string VoteInstruction { get; set; }

        //Managed Account Fees
        public string PortfolioManagementFee { get; set; } = "0";
        public string InitialFee { get; set; } = "0";
        public string AdditionalAdvisorFee { get; set; } = "0";
        public string ForeignInvestmentInitialFee { get; set; } = "0";
        public string ForeignInvestmentAnnualFee { get; set; } = "0";

        //Commisions
        public string AdminFee { get; set; } = "0";

        //Dividend
        public string DividendInstruction { get; set; }
    }
}