namespace DataService.Dto
{
    public class RetirementPlanningReportDto
    {
        public string IncomeNeed { get; set; }
        public string TermYears { get; set; }
        public string Age { get; set; }
        public string EscalationPercent { get; set; }
        public string RetirementAge { get; set; }
        public string TotalNeeds { get; set; }
        public string TotalCapitalNeeds { get; set; }
        public string CapitalizedIncomeNeeds { get; set; }
        public string YearsBeforeRetirement { get; set; }
        public string CapitalNeeds { get; set; }
        public string YearsAfterRetirement { get; set; }
        public string OutstandingLiabilities { get; set; }
        public string LifeExpectancy { get; set; }
        public string AvailableCapital { get; set; }
        public string IncomeAvailableTotal{ get; set; }
        public string InvestmentReturnRate { get; set; }
        public string RiskRating { get; set; }
        public string InflationRate { get; set; }
        public string TotalAvailable { get; set; }
        public string ExhaustionPeriod { get; set; }
        public string IncomeNeedsTotal { get; set; }
        public string DescTotalCapital { get; set; }
        public string TotalCapital { get; set; }
        public string MonthlySavingsRequired { get; set; }
        public string MonthlySavingsEscalating { get; set; }
        public GraphReportDto CapitalGraph { get; set; }
        public GraphReportDto AnnualGraph { get; set; }
    }
}
