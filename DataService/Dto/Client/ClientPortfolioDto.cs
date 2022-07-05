using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class ClientPortfolioDto : ApiResponseDto
    {
        public ClientDto Client { get; set; }
        public ClientFNADto FNA { get; set; }
        public List<InvestmentsDto> Investments { get; set; }
        public RetirementSummaryDto Retirement { get; set; }
        public ProvidingOnDisabilityDto ProvidingDisability { get; set; }
        public ProvidingOnDeathDto ProvidingDeath { get; set; }
        public ProvidingOnDreadDiseaseDto ProvidingDread { get; set; }
        public List<ShortTermInsuranceDTO> ShortTermInsurance { get; set; }
        public MedicalAidDTO MedicalAid { get; set; }
        public List<AssetsAttractingCGTDto> AssetsAttractingCGT { get; set; }
        public List<AssetsExemptFromCGTDto> AssetsExemptFromCGT { get; set; }
        public List<InsuranceDto> Insurance { get; set; }

        public double Salary { get; set; }
        //public double PensionFunds { get; set; }
        //public double PreservationFunds { get; set; }
        public double FuneralCover { get; set; }
        //public double DisabilityLumpSum { get; set; }
        //public double DisabilityTemporary { get; set; }
        //public double DisabilityPermanent { get; set; }
        public double HomeValue { get; set; }
        //public double InvestmentsTotal { get; set; }
        //public double InvestedBonds { get; set; }
        //public double InvestedEuity { get; set; }
        //public double InvestedProperty { get; set; }
        //public double InvestedOffshoreBonds { get; set; }
        //public double InvestedOffshoreEuity { get; set; }
        //public double InvestedOffshoreProperty { get; set; }
        //public double InvestedPrivateEquity { get; set; }
        //public double InvestedCash { get; set; }



    }
}