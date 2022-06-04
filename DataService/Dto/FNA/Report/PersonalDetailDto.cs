using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.FNA.Report
{
    public class PersonalDetailDto
    {
        public string SpacerImgPath { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
        public string RSAIdNumber { get; set; }
        public string SpouseRSAIdNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string ClientAge { get; set; }
        public string SpouseClientAge { get; set; }
        public string Gender { get; set; }
        public string SpouseGender { get; set; }
        public string LifeExpectancy { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseMaritalStatus { get; set; }
        public string DateOfMarriage { get; set; }
        public string SpouseDateOfMarriage { get; set; }
        public string Email { get; set; }
        public string SpouseEmail { get; set; }
        public string WorkNumber { get; set; }
        public string SpouseWorkNumber { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPostal { get; set; }
    }
}
