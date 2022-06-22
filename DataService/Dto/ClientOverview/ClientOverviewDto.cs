using System;

namespace DataService.Dto
{
    public class ClientOverviewDto : ApiResponseDto
    {
        public ClientDto Client { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int Salary { get; set; }
        public int PensionFunds { get; set; }
        public int PreservationFunds { get; set; }
        public int FuneralCover { get; set; }
        public int DisabilityLumpSum { get; set; }
        public int DisabilityTemporary { get; set; }
        public int DisabilityPermanent { get; set; }
        public int MedicalAid { get; set; }
        public int HomeValue { get; set; }
        public int InvestedBonds { get; set; }
        public int InvestedEuity { get; set; }
        public int InvestedProperty { get; set; }
        public int InvestedOffshoreBonds { get; set; }
        public int InvestedOffshoreEuity { get; set; }
        public int InvestedOffshoreProperty { get; set; }
        public int InvestedPrivateEquity { get; set; }
        public int InvestedCash { get; set; }



    }
}