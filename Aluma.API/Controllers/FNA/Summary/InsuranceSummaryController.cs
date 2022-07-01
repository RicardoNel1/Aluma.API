using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class InsuranceSummaryController : ControllerBase
    {
        private readonly IWrapper _repo;

        public InsuranceSummaryController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetInsuranceSummary(int fnaId)
        {
            InsuranceSummaryDto dto = new();
            try
            {
                dto = _repo.InsuranceSummary.GetInsuranceSummary(fnaId);

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
        public IActionResult UpdateInsuranceSummary([FromBody] InsuranceSummaryDto dto)
        {
            try
            {
                dto = _repo.InsuranceSummary.UpdateInsuranceSummary(dto);
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