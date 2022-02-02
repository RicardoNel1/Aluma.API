namespace DataService.Dto
{
    public class AddressDto
    {
        public string UnitNumber { get; set; }
        public string Complex { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool InCareAddress { get; set; }
        public string InCareName { get; set; }
        public int YearsAtAddress { get; set; }
        public string AddressSameAs { get; set; }
    }
}