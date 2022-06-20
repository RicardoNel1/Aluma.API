using System;
using System.Collections.Generic;
using System.Linq;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ShortTermInsuranceController : Controller
    {
        private readonly IWrapper _repo;

        public ShortTermInsuranceController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetShortTerm(int clientId)
        {
            try
            {
                List<ShortTermInsuranceDTO> sortTermInsuranceDTOs = _repo.ShortTermInsurance.GetSortTermInsurance(clientId);
                return Ok(sortTermInsuranceDTOs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateShortTerm([FromBody] List<ShortTermInsuranceDTO> dtoArray)
        {
            try
            {
                dtoArray = _repo.ShortTermInsurance.UpdateSortTermInsurance(dtoArray);

                if (dtoArray.Where(x => x.Status != "Success" && !string.IsNullOrEmpty(x.Status)).Any())
                    return BadRequest(dtoArray);

                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, AllowAnonymous]
        public IActionResult DeleteShortTerm(int id)
        {
            try
            {
                bool deleted = _repo.ShortTermInsurance.DeleteSortTermInsurance(id);
                
                if (deleted)
                    return NoContent();
                
                return BadRequest("Short-term insurance could not be deleted.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}