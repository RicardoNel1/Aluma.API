using System;
using DataService.Enum;

namespace DataService.Dto
{

    public class AdministrationCostsDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string OtherFixedProperty { get; set; }
        public double OtherFixedPropertyValue { get; set; }
        public double OtherConveyanceCosts { get; set; }
        public double AdvertisingCosts { get; set; }
        public double RatesAndTaxes { get; set; }
        public string OtherAdministrationCosts { get; set; }
        public double OtherAdministrationCostsValue { get; set; }
        public double TotalEstimatedCosts { get; set; }
    }

}