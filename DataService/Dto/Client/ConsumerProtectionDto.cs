namespace DataService.Dto
{
    public class ConsumerProtectionDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool InformProducts { get; set; } = true;
        public bool InformOffers { get; set; } = true;
        public bool RequestResearch { get; set; } = true;
        public string PreferredComm { get; set; } = "Email";
        public bool OtherCommMethods { get; set; } = true;

    }
}