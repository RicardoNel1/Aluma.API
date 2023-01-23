namespace DataService.Dto
{
    public class ProvidingOnDisabilityReportDto
    {
        public string IncomeNeed { get; set; }
        public string Age { get; set; }
        public string NeedsDisabilityTerm_Years { get; set; }
        public string RetirementAge { get; set; }
        public string EscDisabilityPercent { get; set; }
        public string LifeExpectancy { get; set; }
        public string CapitalDisabilityNeeds { get; set; }
        public string YearsTillRetirement { get; set; }
        public string InvestmentReturnRate { get; set; }
        public string ShortTermProtectionIncome { get; set; }
        public string TotalAvailable { get; set; }
        public string InflationRate { get; set; }
        public string LongTermProtectionIncome { get; set; }
        public string Capital { get; set; }
        public string CurrentNetIncome { get; set; }
        public string TotalNeeds { get; set; }
        public string CapitalNeeds { get; set; }
        public string CapitalizedIncomeShortfall { get; set; }
        public string AvailableCapital { get; set; }
        public string TotalCapShortfallSurplusDesc { get; set; }
        public string TotalCapShortfallSurplus { get; set; }
        public string MaxAdditionalCap { get; set; }
        public GraphReportDto Graph { get; set; }
    }
}
