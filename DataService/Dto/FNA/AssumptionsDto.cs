namespace DataService.Dto
{
    public class AssumptionsDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public int RetirementAge { get; set; }
        public int LifeExpectancy { get; set; }
        public int YearsTillLifeExpectancy { get; set; }
        public int YearsTillRetirement { get; set; }
        public int YearsAfterRetirement { get; set; }
        public double CurrentGrossIncome { get; set; }
        public double CurrentNetIncome { get; set; }
        public string RetirementInvestmentRisk { get; set; }
        public string DeathInvestmentRisk { get; set; }
        public string DisabilityInvestmentRisk { get; set; }
    }
}
