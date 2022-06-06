﻿using DataService.Dto;
using SelectPdf;
using System;
using System.IO;
using System.Linq;
using Aluma.API.Repositories.FNA.Report.Service;
using Aluma.API.RepoWrapper;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Services.Base

{
    public interface IDocumentBaseService
    {
        string PDFGeneration(string html);
        Task<string> FNAHtmlGeneration(FNAReportDto dto);
    }

    public class DocumentBaseService : IDocumentBaseService
    {
        private readonly IWrapper _repo;
        public DocumentBaseService(IWrapper repo)
        {
            _repo = repo;
        }

        public string PDFGeneration(string html)
        {

            string pdf_page_size = "A4";
            var pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                pdf_page_size, true);

            string pdf_orientation = "Portrait";
            var pdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                pdf_orientation, true);

            int webPageWidth = 1024;
            int webPageHeight = 1356;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new();


            var text = new PdfTextSection(0, 10, "Aluma", new System.Drawing.Font("Open Sans", 8))
            {
                HorizontalAlign = PdfTextHorizontalAlign.Left,

            };


            converter.Footer.Add(text);

            // page numbers can be added using a PdfTextSection object
            text = new PdfTextSection(460, 10, "Page: {page_number} of {total_pages}         ", new System.Drawing.Font("Open Sans", 8))
            {
                HorizontalAlign = PdfTextHorizontalAlign.Center
            };
            converter.Footer.Add(text);


            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 50;

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;
            converter.Options.DrawBackground = false;
            converter.Options.MarginTop = 30;
            converter.Options.MarginLeft = 30;



            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(html);

            MemoryStream ms = new();

            doc.Save(ms);


            byte[] buffer = ms.ToArray();

            string base64 = Convert.ToBase64String(buffer);

            buffer = Convert.FromBase64String(base64);

            doc.Save(ms);

            // close pdf document
            doc.Close();

            byte[] file = ms.ToArray();

            ms.Close();

            return Convert.ToBase64String(file);
        }

        public async Task<string> FNAHtmlGeneration(FNAReportDto dto)
        {

            IFNAModulesService _fNAModulesService = new FNAModulesService();


            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report.html");

                string result = File.ReadAllText(path);

                string logo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/img/aluma-logo-2.png");

                string spacer = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/img/spacer.png");

                result = result.Replace("[name]", "Tiago Van Niekerk").Replace("[date]", DateTime.Now.ToString("dd/MM/yyyy")).Replace("[logo]", logo);


                result += _fNAModulesService.OverviewModule(dto.FNAId);

                if (dto.FNAId == 0)
                    return null;

                if (dto.ClientModule)
                {
                    IClientPersonalInfoService _clientPersonalInfoService = new ClientPersonalInfoService(_repo);
                    result += await _clientPersonalInfoService.SetPersonalDetail(dto.FNAId);
                }

                if (dto.ProvidingOnDisability)
                {
                    IProvidingDisabilityService _providingDisabilityService = new ProvidingDisabilityService(_repo);
                    result += _providingDisabilityService.SetDisabilityDetail(dto.FNAId);
                }

                if (dto.ProvidingOnDreadDisease)
                {
                    IProvidingDreadService _povidingDreadService = new ProvidingDreadService(_repo);
                    result += _povidingDreadService.SetDreadDetail(dto.FNAId);
                }

                if (dto.RetirementPlanning)
                {
                    IProvidingRetirementService _providingRetirementService = new ProvidingRetirementService(_repo);
                    result += _providingRetirementService.SetRetirementDetail(dto.FNAId);
                }


                result += "</body>";


                result += "<script type='text/javascript'>";


                if (dto.ProvidingOnDisability)
                    result += _fNAModulesService.CapitalSolutionGraphJavascript(dto.FNAId);
                result += "</script>";


                result += "</html>";

                result = result.Replace("[spacer]", spacer);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
    }
}
