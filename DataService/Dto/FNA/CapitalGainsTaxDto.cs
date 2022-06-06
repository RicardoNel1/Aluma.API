namespace DataService.Dto
{
    public class CapitalGainsTaxDto
    {
        public int Id { get; set; }
        public int FnaId { get; set; }
        public double PreviousCapitalLosses { get; set; }
        public double TotalCGTPayable { get; set; }
    }
}
