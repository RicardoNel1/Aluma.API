namespace DataService.Dto
{
    public class PurposeAndFundingDto
    {
        public int ApplicationId { get; set; }       
        public bool fundsEmployedSalary { get; set; }
        public bool fundsEmployedCommission { get; set; }
        public bool fundsEmployedBonus { get; set; }
        public bool fundsSelfTurnover { get; set; }
        public bool fundsRetiredAnnuity { get; set; }
        public bool fundsRetiredOnceOff { get; set; }
        public bool fundsDirectorSalary { get; set; }
        public bool fundsDirectorDividend { get; set; }
        public bool fundsDirectorInterest { get; set; }
        public bool fundsDirectorBonus { get; set; }
        public string fundsOther { get; set; }
        public bool wealthIncome { get; set; }
        public bool wealthInvestments { get; set; }
        public bool wealthShares { get; set; }
        public bool wealthProperty { get; set; }
        public bool wealthCompany { get; set; }
        public bool wealthInheritance { get; set; }
        public bool wealthLoan { get; set; }
        public bool wealthGift { get; set; }
        public string wealthOther { get; set; }
        public string InvestmentGoal { get; set; }
    }
}