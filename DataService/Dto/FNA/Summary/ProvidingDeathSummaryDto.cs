namespace DataService.Dto
{
    public class ProvidingDeathSummaryDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double TotalNeeds { get; set; }
        public double TotalAvailable { get; set; }

    }
}