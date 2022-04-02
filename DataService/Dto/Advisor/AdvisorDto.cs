using System;

namespace DataService.Dto
{
    public class AdvisorDto
    {
        public UserDto User { get; set; }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        
        public string Title { get; set; }
        public string BusinessTel { get; set; }
        public string HomeTel { get; set; }
        public string Fax { get; set; }

        //INSURANCE
        public bool AdviceLTSubCatA { get; set; }

        public bool SupervisedLTSubCatA { get; set; }
        public bool AdviceLTSubCatB1 { get; set; }
        public bool SupervisedLTSubCatB1 { get; set; }
        public bool AdviceLTSubCatB1A { get; set; }
        public bool SupervisedLTSubCatB1A { get; set; }
        public bool AdviceLTSubCatB2 { get; set; }
        public bool SupervisedLTSubCatB2 { get; set; }
        public bool AdviceLTSubCatB2A { get; set; }
        public bool SupervisedLTSubCatB2A { get; set; }
        public bool AdviceLTSubCatC { get; set; }
        public bool SupervisedLTSubCatC { get; set; }
        public bool AdviceSTPersonal { get; set; }
        public bool SupervisedSTPersonal { get; set; }
        public bool AdviceSTPersonalA1 { get; set; }
        public bool SupervisedSTPersonalA1 { get; set; }
        public bool AdviceSTCommercial { get; set; }
        public bool SupervisedSTCommercial { get; set; }

        //DEPOSITS
        public bool AdviceLTDeposits { get; set; }

        public bool SupervisedLTDeposits { get; set; }
        public bool AdviceSTDeposits { get; set; }
        public bool SupervisedSTDeposits { get; set; }
        public bool AdviceStructuredDeposits { get; set; }
        public bool SupervisedStructuredDeposits { get; set; }

        //PENSION
        public bool AdviceRetailPension { get; set; }

        public bool SupervisedRetailPension { get; set; }
        public bool AdvicePensionFunds { get; set; }
        public bool SupervisedPensionFunds { get; set; }
        public bool AdviceShares { get; set; }
        public bool SupervisedShares { get; set; }
        public bool AdviceMoneyMarket { get; set; }
        public bool SupervisedMoneyMarket { get; set; }
        public bool AdviceDebentures { get; set; }
        public bool SupervisedDebentures { get; set; }
        public bool AdviceWarrants { get; set; }
        public bool SupervisedWarrants { get; set; }
        public bool AdviceBonds { get; set; }
        public bool SupervisedBonds { get; set; }
        public bool AdviceDerivatives { get; set; }
        public bool SupervisedDerivatives { get; set; }
        public bool AdviceParticipatoryInterestCollective { get; set; }
        public bool SupervisedParticipatoryInterestCollective { get; set; }
        public bool AdviceSecurities { get; set; }
        public bool SupervisedSecurities { get; set; }
        public bool AdviceParticipatoryInterestHedge { get; set; }
        public bool SupervisedParticipatoryInterestHedge { get; set; }
        public bool isExternalBroker { get; set; }
        public bool isSupervised { get; set; }
        public bool isActive { get; set; }

    }
}