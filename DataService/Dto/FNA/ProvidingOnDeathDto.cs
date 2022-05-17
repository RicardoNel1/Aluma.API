using System;
using DataService.Enum;

namespace DataService.Dto
{

    public class ProvidingOnDeathDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double IncomeNeeds { get; set; }
        public int IncomeTerm_Years { get; set; }
        public double CapitalNeeds { get; set; }
        public string Available_InsuranceDescription { get; set; }
        public double Available_Insurance_Amount { get; set; }
        public double RetirementFunds { get; set; }
        public double Available_PreTaxIncome_Amount { get; set; }
        public int Available_PreTaxIncome_Term { get; set; }
    }

}