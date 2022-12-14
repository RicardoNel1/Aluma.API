using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.ClientVerification
{
    public class AMLScreeningSubmitRequestDto
    {
        public string IdNumber { get; set; }
        public string Initial { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string YourReference { get; set; }
    }
}
