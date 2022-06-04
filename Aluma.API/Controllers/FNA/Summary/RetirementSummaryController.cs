using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class RetirementSummaryController : ControllerBase
    {
        private readonly IWrapper _repo;

        public RetirementSummaryController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetRetirementSummary(int fnaId)
        {
            RetirementSummaryDto dto = new();
            try
            {
                dto = _repo.RetirementSummary.GetRetirementSummary(fnaId);

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
        public IActionResult UpdateRetirementSummary([FromBody] RetirementSummaryDto dto)
        {
            try
            {
                dto = _repo.RetirementSummary.UpdateRetirementSummary(dto);
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