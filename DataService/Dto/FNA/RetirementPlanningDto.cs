using System;
using DataService.Enum;

namespace DataService.Dto
{

    public class RetirementPlanningDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double MonthlyIncome { get; set; }
        public int TermPostRetirement_Years { get; set; }
        public double IncomeEscalation { get; set; }
        public double IncomeNeeds { get; set; }
        public int NeedsTerm_Years { get; set; }
        public double IncomeNeedsEscalation { get; set; }
        public double CapitalNeeds { get; set; }
        public double CapitalAvailable { get; set; }
        public double TotalCapitalNeeds { get; set; }
        public double TotalCapitalAvailable { get; set; }
        public double OutstandingLiabilities { get; set; }
        public double SavingsEscalation { get; set; }
    }

}