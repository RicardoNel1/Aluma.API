using System.Collections.Generic;

namespace DataService.Dto
{
    public enum GraphType
    {
        Line,
        Bar,
        Pie,
        Column
    }

    public class GraphReportDto
    {
        public GraphType Type { get; set; }
        public string Name { get; set; }
        public string XaxisHeader { get; set; }
        public string YaxisHeader { get; set; }
        public int Height { get; set; }// = 500;
        public int Width { get; set; } = 710;
        public List<string> Data { get; set; }
    }

    public class GraphResult
    {
        public string Html { get; set; }
        public string Script { get; set; }
    }
}
