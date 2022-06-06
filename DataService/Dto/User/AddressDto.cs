using System;

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
        public bool AddressSameAs { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(StreetNumber) && !string.IsNullOrEmpty(StreetName)
                && !string.IsNullOrEmpty(Suburb) && !string.IsNullOrEmpty(City)
                && !string.IsNullOrEmpty(Country) && !string.IsNullOrEmpty(PostalCode))
            {
                string result = string.IsNullOrEmpty(UnitNumber) && string.IsNullOrEmpty(ComplexName) ? string.Empty : $"{UnitNumber} {ComplexName}";
                result += $"{StreetNumber} {StreetName}, {Environment.NewLine}";
                result += $"{Suburb}, {Environment.NewLine}";
                result += $"{City}, {Environment.NewLine}";
                result += $"{Country}, {Environment.NewLine}";
                result += $"{PostalCode}";

                return result;
            }

            return string.Empty;
        }
    }
}