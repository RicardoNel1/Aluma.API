using System;
using DataService.Enum;

namespace DataService.Dto
{

    public class AssetsAttractingCGTDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        
        public double RecurringPremium { get; set; }
        public double EscPercent { get; set; }
        public double Growth { get; set; }
        public string PropertyType { get; set; }
        public string AllocateTo { get; set; }
        public double BaseCost { get; set; }
         public bool DisposedAtRetirement { get; set; }
        public bool DisposedOnDisability { get; set; }
    }

}