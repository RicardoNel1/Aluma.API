namespace DataService.Dto
{
    public class MaritalDetailsDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string MaritalStatus { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MaidenName { get; set; }
        public string IdNumber { get; set; }
        public string DateOfMarriage { get; set; }
        public bool ForeignMarriage { get; set; }
        public string CountryOfMarriage { get; set; }
        public string SpouseDateOfBirth { get; set; }
        public bool PowerOfAttorney { get; set; }
    }
}