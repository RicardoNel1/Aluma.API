﻿using DataService.Dto;
using System;
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
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\graph.html");
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

            string js = $"{Environment.NewLine} google.charts.setOnLoadCallback({functionName});";
            js += $"function {functionName}() {{";


            switch (dto.Type)
            {
                case GraphType.Pie:
                    js += $"{Environment.NewLine} {GetPieChart(dto)} {Environment.NewLine}";
                    break;
                case GraphType.Column:
                    js += $"{Environment.NewLine} {GetColumnChart(dto)} {Environment.NewLine}";
                    break;
                    js += $"{Environment.NewLine}";
                case GraphType.Line:
                    break;
                    js += $"{Environment.NewLine}";
                case GraphType.Bar:
                    break;
                default:
                    js += $"{Environment.NewLine} var chart = new google.visualization.LineChart(document.getElementById('Graph_{dto.Name.Replace(" ", "_")}'));";
                    break;
            }

            js += $"}} {Environment.NewLine}";

            return js;
        }

        private static string GetPieChart(GraphReportDto dto)
        {
            string js = $"var data = new google.visualization.arrayToDataTable([";
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
            js += $"var options = {{ title: '{dto.Name}',  pieSliceText: 'none', legend: {{ position: 'labeled', labeledValueText: 'both', }} }}; ";
            js += "chart.draw(data, options);";

            return js;
        }

        private static string GetColumnChart(GraphReportDto dto)
        {
            string js = $"var data = new google.visualization.arrayToDataTable([";
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
            js += "var view = new google.visualization.DataView(data);";
            js += "view.setColumns([0,1]);";
            js += $"var options = {{ title: '{dto.Name}', width: {dto.Width}, height: {dto.Height}, bar: {{groupWidth: \"85%\"}}, legend: {{ position: \"bottom\" }} }}; ";
            js += $"var chart = new google.visualization.ColumnChart(document.getElementById('Graph_{dto.Name.Replace(" ", "_")}'));";
            js += "chart.draw(view, options);";

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
