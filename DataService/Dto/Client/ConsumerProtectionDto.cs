using System;
using DataService.Enum;

namespace DataService.Dto
{
    public class ConsumerProtectionDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool InformProducts { get; set; }
        public bool InformOffers { get; set; }
        public bool RequestResearch { get; set; }     
        public string PreferredComm { get; set; }
        public bool OtherCommMethods { get; set; }

    }
}