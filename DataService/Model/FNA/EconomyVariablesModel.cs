using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_economy")]
    public class EconomyVariablesModel : BaseModel
    {    
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double InflationRate { get; set; }
        public double InvestmentReturnRate { get; set; }
    }

}