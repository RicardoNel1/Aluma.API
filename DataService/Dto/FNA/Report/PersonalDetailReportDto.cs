using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class PersonalDetailReportDto
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string RSAIdNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string LifeExpectancy { get; set; }
        public string MaritalStatus { get; set; }
        public string DateOfMarriage { get; set; }
        public string Email { get; set; }
        public string WorkNumber { get; set; }
        public string Address { get; set; }
        public string Postal { get; set; }
    }
}
