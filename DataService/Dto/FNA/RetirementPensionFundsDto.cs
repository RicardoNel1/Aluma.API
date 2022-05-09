namespace DataService.Dto
{

    public class RetirementPensionFundsDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double MonthlyContributions { get; set; }
        public double EscPercent { get; set; }
    }

}