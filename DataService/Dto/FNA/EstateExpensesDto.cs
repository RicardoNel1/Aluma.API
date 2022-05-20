using System;

namespace DataService.Dto
{

    public class EstateExpensesDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double AdminCosts { get; set; }
        public double FuneralExpenses { get; set; }
        public double CashBequests { get; set; }
        public double Other { get; set; }
        public double ExecutorsFees { get; set; }
        public double TotalEstateExpenses { get; set; }
    }

}