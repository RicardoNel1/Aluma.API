using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.Client
{
    public class FinancialProviderDto : ApiResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
