using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.FNA.Report
{
    public class GraphDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<Dictionary<string, string>> Data { get; set; }
    }
}
