namespace DataService.Dto
{
    public class RetirementSummaryDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double TotalPensionFund { get; set; }
        public double TotalPreservation { get; set; }
        public double TotalNeeds { get; set; }
        public double TotalAvailable { get; set; }
        public double SavingsRequiredPremium { get; set; }
    }
}