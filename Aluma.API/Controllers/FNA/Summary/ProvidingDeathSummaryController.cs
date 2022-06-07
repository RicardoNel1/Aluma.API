using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ProvidingDeathSummaryController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ProvidingDeathSummaryController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetProvidingDeathSummary(int fnaId)
        {
            ProvidingDeathSummaryDto dto = new();
            try
            {
                dto = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
                if (dto != null)
                {
                    dto.Status = "Success";
                    return Ok(dto);
                }

                dto = new ProvidingDeathSummaryDto
                {
                    Message = "No records found",
                    Status = "Falure"
                };

                return StatusCode(500, dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateProvidingDeathSummary([FromBody] ProvidingDeathSummaryDto dto)
        {
            try
            {
                dto = _repo.ProvidingDeathSummary.UpdateProvidingDeathSummary(dto);
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


    }
}