using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class FNAController : ControllerBase
    {
        private readonly IWrapper _repo;
        private IDocumentBaseService _documentService;

        public FNAController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _documentService = new DocumentBaseService(_repo, mapper);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetFNA(int clientId)
        {
            ClientFNADto dto = new();
            try
            {
                dto = _repo.FNA.GetClientFNA(clientId);

                dto.Status = "Success";
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpGet("list"), AllowAnonymous]
        public IActionResult GetFNAList(int clientId)
        {
            try
            {
                var fnas = _repo.FNA.GetClientFNAList(clientId);

                return Ok(fnas);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateClientFNA([FromBody] ClientFNADto dto)
        {
            try
            {
                var fnaExist = _repo.Client.CheckForFNA(new ClientDto() { Id = dto.ClientId });
                if (fnaExist.hasFNA)
                {
                    dto.Status = "Failure";
                    dto.Message = "FNA Exists";
                    return BadRequest(dto);
                }

                dto = await _repo.FNA.CreateFNA(dto);
                dto.Status = "Success";
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = "";
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("get_fna_report"), DisableRequestSizeLimit, AllowAnonymous]
        public async Task<IActionResult> GetFNAReport(int fnaId, bool clientModule = true, bool providingOnDisability = true, bool providingOnDreadDisease = true, bool providingOnDeath = true, bool retirementPlanning = true)
        {
            try
            {

                FNAReportDto dto = new FNAReportDto()
                {
                    FNAId = fnaId,
                    ClientModule = clientModule,
                    ProvidingOnDisability = providingOnDisability,
                    ProvidingOnDreadDisease = providingOnDreadDisease,
                    ProvidingOnDeath = providingOnDeath,
                    RetirementPlanning = retirementPlanning
                };

                var urlBuilder = new UriBuilder($"{Request.Scheme}://{Request.Host.Value}");
                // var base64result = _documentService.PDFGeneration(await _documentService.FNAHtmlGeneration(dto, urlBuilder.ToString()));

                // byte[] pdf = Convert.FromBase64String(base64result);

                // if (pdf != null && pdf.Length > 0)
                // {
                //     Stream stream = new MemoryStream(Convert.FromBase64String(base64result));
                //     stream.Position = 0;

                //     return File(stream, MediaTypeNames.Application.Octet, "FNA Report.pdf");
                // }

                string report = await _documentService.FNAHtmlGeneration(dto, urlBuilder.ToString()); 

                if (report.Length > 0) {
                    return Ok(report);
                }

                // return BadRequest("Could not download the 'FNA Report.pdf'");
                return BadRequest("Could not generate the data for the FNA Report");
            }
            catch (Exception ex)
            {
                // return BadRequest($"Could not download the 'FNA Report.pdf', {ex.Message}, {ex.InnerException?.Message}");
                return BadRequest($"Could not generate the data for the FNA Report, {ex.Message}, {ex.InnerException?.Message}");
            }
        }

        [HttpGet("save_fna_report"), DisableRequestSizeLimit, AllowAnonymous]
        public async Task<IActionResult> SaveFNAReport(int fnaId, bool clientModule = true, bool providingOnDisability = true, bool providingOnDreadDisease = true, bool providingOnDeath = true, bool petirementPlanning = true)
        {
            try
            {
                FNAReportDto dto = new FNAReportDto()
                {
                    FNAId = fnaId,
                    ClientModule = clientModule,
                    ProvidingOnDisability = providingOnDisability,
                    ProvidingOnDreadDisease = providingOnDreadDisease,
                    ProvidingOnDeath = providingOnDeath,
                    RetirementPlanning = petirementPlanning
                };

                //var urlBuilder = new UriBuilder(Request.PathBase);
                var urlBuilder = new UriBuilder($"{Request.Scheme}://{Request.Host.Value}");
                await _documentService.SavePDF(dto, urlBuilder.ToString());

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}