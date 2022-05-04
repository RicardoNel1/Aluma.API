using System;

namespace DataService.Dto
{
    public class ClientDto : ApiResponseDto
    {
        public UserDto User { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.Nullable<int> AdvisorId { get; set; }
        public string AdvisorName { get; set; }
        public string ClientType { get; set; }
        public string Title { get; set; }
        //public string Initials { get; set; }
        public string CountryOfResidence { get; set; }
        public string CountryOfBirth { get; set; }        
        public string CityOfBirth { get; set; }
        public string Nationality { get; set; }
        public string EmploymentStatus { get; set; }
        public string Employer { get; set; }
        public string Industry { get; set; }
        public string Occupation { get; set; }
        public string WorkNumber { get; set; }
        public string MaritalStatus { get; set; }
        public string DateOfMarriage { get; set; }
        public bool ForeignMarriage { get; set; }
        public string CountryOfMarriage { get; set; }
        public string SpouseName { get; set; }
        public string MaidenName { get; set; }
        public string SpouseDateOfBirth { get; set; }
        public bool PowerOfAttorney { get; set; }
        public bool NonResidentialAccount { get; set; }
        public bool hasDisclosure { get; set; }
        public DateTime DisclosureDate { get; set; }
        public int ApplicationCount { get; set; }
    }
}