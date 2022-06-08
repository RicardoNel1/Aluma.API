using DataService.Dto;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aluma.API.Repositories.FNA.Report.Services.Base
{
    public interface IGraphService
    {
        GraphResult SetGraphHtml(GraphReportDto dto);
        string InitializeGraphJavaScript();
        string CloseGraphJavaScript();
    }

    public class GraphService : IGraphService
    {
        public GraphResult SetGraphHtml(GraphReportDto dto)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/graph.html");
            string result = File.ReadAllText(path);

            string js = SetGraphScript(dto);

            result = result.Replace("[Id]", $"Graph_{dto.Name.Replace(" ", "_")}");
            result = result.Replace("[Width]", dto.Width < 0 ? $"{dto.Width * -1}" : dto.Width.ToString());
            result = result.Replace("[Height]", dto.Height < 0 ? $"{dto.Height * -1}" : dto.Height.ToString());
            return new()
            {
                Html = result,
                Script = js
            };
        }

        public string SetGraphScript(GraphReportDto dto)
        {
            string functionName = $"drawGraph_{dto.Name.Replace(" ", "_")}Chart";

            string js = $"google.charts.setOnLoadCallback({functionName});";
            js += $"function {functionName}() {{";
            js += $"var options = {{ title: '{dto.Name}' }};";
            

            switch (dto.Type)
            {
                case GraphType.Bar:
                    js += $"var chart = new google.visualization.BarChart(document.getElementById('Graph_{dto.Name.Replace(" ", "_")}'));";
                    break;

                case GraphType.Line:
                    js += $"var data = new google.visualization.DataTable();";
                    js += $"data.addColumn('string', '{dto.XaxisHeader}');";
                    js += $"data.addColumn('number', '{dto.YaxisHeader}');";
                    js += "data.addColumn({ type: 'string', role: 'annotation' });";
                    js += "data.addRows([";





                    js += "]);";
                    js += $"var chart = new google.visualization.LineChart(document.getElementById('Graph_{dto.Name.Replace(" ", "_")}'));";
                    break;

                case GraphType.Pie:
                    {
                        js += $"var data = new google.visualization.arrayToDataTable([";
                        js += $"['{dto.XaxisHeader}', '{dto.YaxisHeader}'],";

                        if (dto.Data != null && dto.Data.Count > 0)
                        {
                            foreach (string kvp in dto.Data)
                            {
                                string[] values = kvp.Split(",");
                                js += $"[";
                                for (int i = 0; i < values.Length; i++)
                                {
                                    if (i == 0)
                                        js += $"'{values[i]}'";
                                    else
                                        js += $", {values[i]}";

                                }
                                js += "],";
                            }
                        }

                        js += "]);";
                        js += $"var chart = new google.visualization.PieChart(document.getElementById('Graph_{dto.Name.Replace(" ", "_")}'));";
                        js += $"options = {{ title: '{dto.Name}',  pieSliceText: 'none', legend: {{ position: 'labeled', labeledValueText: 'both', }} }}; ";
                    }
                    break;

                case GraphType.Column:
                    {
                        js += $"var chart = new google.visualization.ColumnChart(document.getElementById('Graph_{dto.Name.Replace(" ", "_")}'));";
                        js += $"options = {{ title: '{dto.Name}',  pieSliceText: 'none', legend: {{ position: 'labeled', labeledValueText: 'both', }} }}; ";
                    }
                    break;

                default:
                    js += $"var chart = new google.visualization.LineChart(document.getElementById('Graph_{dto.Name.Replace(" ", "_")}'));";
                    break;
            }


            js += "chart.draw(data, options);";
            js += "}";

            return js;
        }

        public string InitializeGraphJavaScript()
        {
            string script = "<script type='text/javascript'>";
            script = $"{script} google.charts.load(\"current\", {{ packages: [\"corechart\"] }});";

            return script;
        }

        public string CloseGraphJavaScript()
        {
            string script = "</script>";
            return script;
        }
    }
}
