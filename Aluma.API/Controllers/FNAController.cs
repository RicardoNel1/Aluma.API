using Aluma.API.RepoWrapper;
using DataService.Dto;
using DocumentService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class FNAController : ControllerBase
    {
        private readonly IWrapper _repo;

        public FNAController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetFNA(int clientId)
        {
            ClientFNADto dto = new ClientFNADto();
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

        [HttpGet, AllowAnonymous]
        public IActionResult GetFNAReport(int clientId)
        {
            FNAReportDto dto = new FNAReportDto()
            {
                ClientId = clientId,
                ClientModule = true
            };
            try
            {
                IDocumentBaseService _documentService = new DocumentBaseService();

                var result = _documentService.PDFGeneration(_documentService.FNAHtmlGeneration(dto));


                return StatusCode(200, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, dto);
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


    }
}