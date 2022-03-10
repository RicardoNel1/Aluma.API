namespace DataService.Dto
{
    public class AddressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UnitNumber { get; set; }
        public string ComplexName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public bool InCareAddress { get; set; }
        public string InCareName { get; set; }
        public int YearsAtAddress { get; set; }
        public string AddressSameAs { get; set; }
    }
}