using System;

namespace DataService.Dto
{
    public class ClientDto : ApiResponseDto
    {
        public UserDto User { get; set; }
        public EmploymentDetailsDto EmploymentDetails { get; set; }
        public MaritalDetailsDto MaritalDetails { get; set; }
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
        public bool NonResidentialAccount { get; set; }
        public bool hasDisclosure { get; set; }
        public DateTime DisclosureDate { get; set; }
        public bool hasFNA { get; set; }
        public DateTime FNADate { get; set; }
        public int ApplicationCount { get; set; }
    }
}