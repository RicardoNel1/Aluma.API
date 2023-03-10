namespace DataService.Dto
{
    public class InsuranceSummaryDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double TotalToSpouse { get; set; }
        public double TotalToThirdParty { get; set; }
        public double TotalToLiquidity { get; set; }
    }
}