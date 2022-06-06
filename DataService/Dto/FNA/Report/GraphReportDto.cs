using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public enum GraphType
    {
        Line,
        Bar,
        Pie
    }

    public class GraphReportDto
    {
        public GraphType Type { get; set; }
        public string Name { get; set; }
        public string XaxisHeader { get; set; }
        public string YaxisHeader { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }

    public class GraphResult
    {
        public string Html { get; set; }
        public string Script { get; set; }
    }
}
