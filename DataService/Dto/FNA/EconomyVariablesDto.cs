namespace DataService.Dto
{
    public class EconomyVariablesDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }                                  //@Justin this field does not exist in the model
        public double InflationRate { get; set; }
        public double InvestmentReturnRate { get; set; }                //@Justin this field does not exist in the model
    }
}


