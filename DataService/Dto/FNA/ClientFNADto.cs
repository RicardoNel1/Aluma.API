using DataService.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{    
    public class ClientFNADto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int AdvisorId { get; set; }
    }
}
