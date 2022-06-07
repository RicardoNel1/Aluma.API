using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_summary_assets")]
    public class AssetSummaryModel : BaseModel
    { 
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double TotalAssetsAttractingCGT { get; set; }
        public double TotalAssetsExcemptCGT { get; set; }
        public double TotalLiquidAssets{ get; set; }
        public double TotalAssetsToEstate { get; set; }
        public double TotalAccrual{ get; set; }
        public double TotalLiabilities{ get; set; }
    }
}