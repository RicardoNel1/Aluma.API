using System;
using DataService.Enum;

namespace DataService.Dto
{
    public class ConsumerProtectionDto
    {
        public ClientDto Client { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool InformProducts { get; set; }
        public bool InformOffers { get; set; }
        public bool RequestResearch { get; set; }     
        public CommEnum PreferredComm { get; set; }
        public bool OtherCommMethods { get; set; }

    }
}