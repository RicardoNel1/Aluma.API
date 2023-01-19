using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    internal class CompletedFNADto : ApiResponseDto
    {
        public string Client { get; set; }
        public string Advisor { get; set; }
        public DateTime Created { get; set; }
    }
}
