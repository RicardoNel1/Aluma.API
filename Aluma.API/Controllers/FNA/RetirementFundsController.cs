using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class RetirementFundsController : ControllerBase
    {
        private readonly IWrapper _repo;

        public RetirementFundsController(IWrapper repo)
        {
            _repo = repo;
        }


        //Pension Funds      
        [HttpPut("pension_funds"), AllowAnonymous]
        public IActionResult UpdateRetirementPensionFunds([FromBody] List<RetirementPensionFundsDto> dtoArray)
        {
            try
            {
                dtoArray = _repo.RetirementPensionFunds.UpdateRetirementPensionFunds(dtoArray);

                if (dtoArray.Where(x => x.Status != "Success" && !string.IsNullOrEmpty(x.Status)).Any())
                    return BadRequest(dtoArray);

                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("pension_funds"), AllowAnonymous]
        public IActionResult GetRetirementPensionFunds(int fnaId)
        {
            try
            {
                List<RetirementPensionFundsDto> dtoList = _repo.RetirementPensionFunds.GetRetirementPensionFunds(fnaId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //Preservation Funds    
        [HttpPut("preservation_funds"), AllowAnonymous]
        public IActionResult UpdateRetirementPreservationFunds([FromBody] List<RetirementPreservationFundsDto> dtoArray)
        {
            try
            {
                dtoArray = _repo.RetirementPreservationFunds.UpdateRetirementPreservationFunds(dtoArray);
                
                if (dtoArray.Where(x => x.Status != "Success" && !string.IsNullOrEmpty(x.Status)).Any())
                    return BadRequest(dtoArray);

                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("preservation_funds"), AllowAnonymous]
        public IActionResult GetRetirementPreservationFunds(int fnaId)
        {
            try
            {
                List<RetirementPreservationFundsDto> dtoList = _repo.RetirementPreservationFunds.GetRetirementPreservationFunds(fnaId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}