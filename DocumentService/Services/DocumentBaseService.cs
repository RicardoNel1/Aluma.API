using DataService.Dto;
using DocumentService.Interfaces;
using SelectPdf;
using System;
using System.IO;
using System.Linq;

namespace DocumentService.Services
{
    public class DocumentService : IDocumentService
    {
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
            int webPageHeight = 0;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new();

            //var footerHtml = new PdfHtmlSection("<footer><span style='font-family: Helvetica, Arial, sans-serif; '></span></footer>", "")
            //{
            //    AutoFitHeight = HtmlToPdfPageFitMode.AutoFit
            //};

            var text = new PdfTextSection(31, 10, "LNDR (PTY) LTD is a registered Credit Provider (NCRCP9333)  ", new System.Drawing.Font("Arial", 8))
            {
                HorizontalAlign = PdfTextHorizontalAlign.Left,

            };
            //if (officeID == 41 || officeID == 42)
            //{
            //    text = new PdfTextSection(31, 10, "ubank Limited is an authorised Financial Services Provider (FSP No.14740) and Credit Provider (NCRCP21)  ", new System.Drawing.Font("Arial", 8))
            //    {
            //        HorizontalAlign = PdfTextHorizontalAlign.Left,

            //    };
            //}

            converter.Footer.Add(text);

            // page numbers can be added using a PdfTextSection object
            text = new PdfTextSection(520, 10, "Page: {page_number} of {total_pages}         ", new System.Drawing.Font("Arial", 8))
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

            string[] HTMLstring = html.Cast<string>().ToArray();
            string HTMLStrings = string.Join(" ", HTMLstring);


            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HTMLStrings);

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

        public string FNAHtmlGeneration(FNAReportDto dto)
        {

            IFNAModulesService _fNAModulesService = new FNAModulesService();

            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media/fna_report.html");

                string result = File.ReadAllText(path);

                if (dto.ClientId == 0)
                    return null;

                if (dto.ClientModule)
                    result += _fNAModulesService.ClientModule(dto.ClientId);

                result.Replace("%FNADate%", DateTime.Now.ToString("yyyy/MM/dd"))
                    .Replace("%FullName%", "FullName");

                result += "</body></html>";

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
