using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class TaxResidencyDto
    {
        //public List<ForeignTaxResidencyDto> TaxResidencyItems { get; set; }

        public ForeignTaxResidencyDto TaxResidencyItems { get; set; }


        public int Id { get; set; }
        public int ClientId { get; set; }
        public string TaxNumber { get; set; }
        public bool TaxObligations { get; set; }
        public bool UsCitizen { get; set; }
        public bool UsRelinquished { get; set; }
        public bool UsOther { get; set; }
    }

    public class ForeignTaxResidencyDto
    {
        public int Id { get; set; }     //added
        public int TaxResidencyId { get; set; } //added
        public string Country { get; set; }
        public string TinNumber { get; set; }
        public string TinUnavailableReason { get; set; }
    }
}