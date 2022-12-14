using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class ClientVerificationServiceDto
    {
        public string BaseUrl { get; set; }
        public string Authorization { get; set; }
        public string Memberkey { get; set; }
        public string Password { get; set; }
    }
}
