namespace DataService.Dto
{

    public class AssetsExemptFromCGTDto: ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double Growth { get; set; }
        public bool DisposedAtRetirement { get; set; }
        public bool DisposedOnDisability { get; set; }
        public string AllocateTo { get; set; }
    }

}