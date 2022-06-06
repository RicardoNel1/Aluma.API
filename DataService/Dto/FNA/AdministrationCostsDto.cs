namespace DataService.Dto
{

    public class AdministrationCostsDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double OtherConveyanceCosts { get; set; }
        public double AdvertisingCosts { get; set; }
        public double RatesAndTaxes { get; set; }
        public string OtherAdminDescription { get; set; }
        public double OtherAdminCosts { get; set; }
        public double TotalEstimatedCosts { get; set; }
    }

}