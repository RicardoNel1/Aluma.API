using System;
using DataService.Enum;

namespace DataService.Dto
{

    public class AssetsAttractingCGTDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string AllocateTo { get; set; }
        public double BaseCost { get; set; }
    }

}