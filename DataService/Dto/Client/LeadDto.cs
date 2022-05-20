namespace DataService.Dto
{
    public class LeadDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int AdvisorId { get; set; }
    }
}