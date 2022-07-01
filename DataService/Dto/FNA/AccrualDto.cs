namespace DataService.Dto
{
    //stuff that has to be saved
    public class AccrualDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double ClientAssetsCommencement { get; set; }
        public double SpouseAssetsCommencement { get; set; }
        public double ClientEstateCurrent { get; set; }
        public double SpouseEstateCurrent { get; set; }
        public double ClientLiabilities { get; set; }
        public double SpouseLiabilities { get; set; }
        public double ClientExcludedValue { get; set; }
        public double SpouseExcludedValue { get; set; }
        public double Offset { get; set; }
        public string? AllocateTo { get; set; }
        public double TotalAsAsset { get; set; }
        public double TotalAsLiability { get; set; }

    }
}
