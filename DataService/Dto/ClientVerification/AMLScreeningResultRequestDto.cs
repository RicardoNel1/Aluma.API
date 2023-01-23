using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.ClientVerification
{
    public class AMLScreeningResultRequestDto
    {
        public string EnquiryID { get; set; }
        public string EnquiryResultID { get; set; }
        public string YourReference { get; set; }

    }
}
