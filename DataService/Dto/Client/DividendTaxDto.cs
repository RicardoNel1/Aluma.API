using System;

namespace DataService.Dto
{
    public class DividendTaxDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string NameSurname { get; set; }
        public string TradingName { get; set; }
        public string AccountReference { get; set; }
        public string NatureOfEntity { get; set; }
        public string IdNo { get; set; }
        public string DateOfBirth { get; set; }
        public string TaxNo { get; set; }
        public string Address { get; set; }
        public string Postal { get; set; }
        public string Country { get; set; }
        public string TitleSurname { get; set; }
        public string InitialsFirstName { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string IdNoPassport { get; set; }
        public string Work { get; set; }
        public string Home { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Exemption { get; set; }
    }
}