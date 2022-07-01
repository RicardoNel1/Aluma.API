using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ProvidingDisabilitySummaryController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ProvidingDisabilitySummaryController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetProvidingDisabilitySummary(int fnaId)
        {
            ProvidingDisabilitySummaryDto dto = new();
            try
            {
                dto = _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);

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

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateProvidingDisabilitySummary([FromBody] ProvidingDisabilitySummaryDto dto)
        {
            try
            {
                dto = _repo.ProvidingDisabilitySummary.UpdateProvidingDisabilitySummary(dto);
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