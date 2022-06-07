﻿using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
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

        public FNAController(IWrapper repo)
        {
            _repo = repo;
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
        public async Task<IActionResult> GetFNAReport( int fnaId, bool clientModule = true, bool providingOnDisability = true, bool providingOnDreadDisease = true, bool providingOnDeath = true, bool petirementPlanning = true)
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

                IDocumentBaseService _documentService = new DocumentBaseService(_repo);
                var base64result = _documentService.PDFGeneration(await _documentService.FNAHtmlGeneration(dto));
                byte[] pdf = Convert.FromBase64String(base64result);

                if (pdf != null && pdf.Length > 0)
                {
                    Stream stream = new MemoryStream(Convert.FromBase64String(base64result));
                    stream.Position = 0;

                    return File(stream, MediaTypeNames.Application.Octet, "FNA Report.pdf");
                }

                return BadRequest("Could not download the 'FNA Report.pdf'");
            }
            catch (Exception e)
            {
                return BadRequest("Could not download the 'FNA Report.pdf'");
            }
        }
    }
}