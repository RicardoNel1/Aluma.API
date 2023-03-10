namespace DataService.Dto
{

    public class RetirementPensionFundsDto: ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double MonthlyContributions { get; set; }
        public double EscPercent { get; set; }
        public double Growth { get; set; }
    }

}