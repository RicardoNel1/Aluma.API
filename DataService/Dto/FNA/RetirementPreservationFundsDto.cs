namespace DataService.Dto
{

    public class RetirementPreservationFundsDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double Growth { get; set; }

    }

}