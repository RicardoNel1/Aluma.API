using DataService.Dto;
using SelectPdf;
using System;
using System.IO;
using System.Linq;
using Aluma.API.Repositories.FNA.Report.Service;
using Aluma.API.RepoWrapper;
using System.Threading.Tasks;
using System.Web;
using Aluma.API.Helpers;
using DataService.Model;
using AutoMapper;
using DataService.Enum;
using iText.Html2pdf;
//using iText.Kernel.Pdf;
using iText.Layout;

namespace Aluma.API.Repositories.FNA.Report.Services.Base

{
    public interface IDocumentBaseService
    {
        string PDFGeneration(string html);
        Task<string> FNAHtmlGeneration(FNAReportDto dto);
        Task SavePDF(FNAReportDto dto);
    }

    public class DocumentBaseService : IDocumentBaseService
    {
        private readonly IWrapper _repo;
        private readonly IMapper _mapper;

        public DocumentBaseService(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task SavePDF(FNAReportDto dto)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(dto.FNAId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = await _repo.User.GetUserWithAddress(new() { Id = client.UserId });
                UserModel userModel = _mapper.Map<UserModel>(user);


                string pdf = PDFGeneration(await FNAHtmlGeneration(dto));
                pdf = EncryptFileData(pdf);

                byte[] pdfBytes = Convert.FromBase64String(pdf);
                await _repo.DocumentHelper.SaveDocument(pdfBytes, DocumentTypesEnum.FNAReport, userModel);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                throw;
            }
        }

        public string PDFGeneration(string html)
        {
            try
            {
                string pdf_page_size = "A4";
                var pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                    pdf_page_size, true);

                string pdf_orientation = "Portrait";
                var pdfOrientation =
                    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                    pdf_orientation, true);


                // instantiate a html to pdf converter object
                HtmlToPdf converter = new();
                MemoryStream ms = new();

                var text = new PdfTextSection(450, 10, "Page: {page_number} of {total_pages}         ", new System.Drawing.Font("Open Sans", 8))
                {
                    HorizontalAlign = PdfTextHorizontalAlign.Center
                };


                converter.Footer.Add(text);

                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = false;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = 40;

                // set converter options
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfOrientation;
                converter.Options.DrawBackground = false;

                // create a new pdf document converting an url
                PdfDocument doc = converter.ConvertHtmlString(html);


                //TODO: doc.Security.UserPassword = client.user.rsaid

                doc.Save(ms);

                //byte[] buffer = ms.ToArray();

                //base64 = Convert.ToBase64String(buffer);

                //buffer = Convert.FromBase64String(base64);

                //doc.Save(ms);

                // close pdf document
                doc.Close();

                byte[] file = ms.ToArray();

                ms.Close();

                return Convert.ToBase64String(file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<string> FNAHtmlGeneration(FNAReportDto dto)
        {
            IFNAModulesService _fNAModulesService = new FNAModulesService(_repo);
            IGraphService _graphService = new GraphService();
            ISummaryService _summaryService = new SummaryService(_repo);

            try
            {
                if (dto.FNAId == 0)
                    return null;

                string version = "1.0";
                string logo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\img\aluma-logo-2.png");
                string frontCover = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\img\front-cover.jpg");
                string spacer = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\img\spacer.png");
                string graph = _graphService.InitializeGraphJavaScript();

                string result = await _fNAModulesService.GetCoverPage(dto.FNAId);
                string css = _fNAModulesService.GetCSS();


                IClientPersonalInfoService _clientPersonalInfoService = new ClientPersonalInfoService(_repo);
                result += await _clientPersonalInfoService.SetPersonalDetail(dto.FNAId);
                result += await _summaryService.SetSummaryDetail(dto.FNAId);


                if (dto.ProvidingOnDeath)
                {
                    IProvidingDeathService _providingDeathService = new ProvidingDeathService(_repo);
                    ReportServiceResult serviceResult = await _providingDeathService.SetDeathDetail(dto.FNAId);

                    result += serviceResult.Html == null ? string.Empty : serviceResult.Html;
                    graph += serviceResult.Script == null ? string.Empty : serviceResult.Script;
                }

                if (dto.ProvidingOnDisability)
                {
                    IProvidingDisabilityService _providingDisabilityService = new ProvidingDisabilityService(_repo);
                    ReportServiceResult serviceResult = await _providingDisabilityService.SetDisabilityDetail(dto.FNAId);

                    result += serviceResult.Html == null ? string.Empty : serviceResult.Html;
                    graph += serviceResult.Script == null ? string.Empty : serviceResult.Script;
                }

                if (dto.ProvidingOnDreadDisease)
                {
                    IProvidingDreadService _povidingDreadService = new ProvidingDreadService(_repo);
                    result += await _povidingDreadService.SetDreadDetail(dto.FNAId);
                }

                if (dto.RetirementPlanning)
                {
                    IProvidingRetirementService _providingRetirementService = new ProvidingRetirementService(_repo);
                    ReportServiceResult serviceResult = await _providingRetirementService.SetRetirementDetail(dto.FNAId);

                    result += serviceResult.Html == null ? string.Empty : serviceResult.Html;
                    graph += serviceResult.Script == null ? string.Empty : serviceResult.Script;
                }

                result += $"{Environment.NewLine}</body>{Environment.NewLine}";
                graph += _graphService.CloseGraphJavaScript();
                result += graph;
                result += $"{Environment.NewLine}</html>";

                result = result.Replace("[date]", DateTime.Now.ToString("yyyy/MM/dd"));
                result = result.Replace("[logo]", logo);
                result = result.Replace("[css]", css);
                result = result.Replace("[frontCover]", frontCover);
                result = result.Replace("[version]", version);
                result = result.Replace("[spacer]", spacer);

                result = HttpUtility.HtmlDecode(result);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private string EncryptFileData(string base64)
        {
            return base64;
        }

        private string DencryptFileData(string base64)
        {
            return base64;
        }

        private void createPdf(string baseUri, string src, string dest)
        {
            //iText.Html2pdf.HtmlConverter converter = new();

            //ConverterProperties properties = new ConverterProperties();
            //properties.SetBaseUri("");
            //MemoryStream ms = new();
            //PdfWriter writer = new PdfWriter(ms);
            //iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer);
            //Document doc = HtmlConverter.ConvertToDocument(html, ms, properties);

            //PdfFooter footer = new();                
            //footer.DisplayOnFirstPage = false;
            //footer.DisplayOnOddPages = true;
            //footer.DisplayOnEvenPages = true;
            //footer.Height = 40;

            //doc.Add(footer);

            //byte[] test = ms.ToArray();

            //string base64 = Convert.ToBase64String(test);

            //document.Close();
        }
    }
}
