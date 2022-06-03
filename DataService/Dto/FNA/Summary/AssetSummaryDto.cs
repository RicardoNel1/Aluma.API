namespace Aluma.API.Repositories
{
    public class AssetSummaryDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double TotalAssetsAttractingCGT { get; set; }
        public double TotalAssetsExcemptCGT { get; set; }
        public double TotalLiquidAssets { get; set; }
        public double TotalAccrual { get; set; }
        public double TotalLiabilities { get; set; }
    }
}