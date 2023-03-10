namespace DataService.Dto
{
    public class ProvidingDisabilitySummaryDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double TotalIncomeNeed { get; set; }
        public double TotalNeeds { get; set; }
        public double TotalAvailable { get; set; }
        public double TotalExistingShortTermIncome { get; set; }
        public double TotalExistingLongTermIncome { get; set; }
    }
}