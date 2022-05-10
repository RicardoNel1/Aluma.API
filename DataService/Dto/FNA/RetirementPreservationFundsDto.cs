using System;

namespace DataService.Dto
{

    public class RetirementPreservationFundsDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double Growth { get; set; }

    }

}