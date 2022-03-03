using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class TaxResidencyController : ControllerBase
    {
        private readonly IWrapper _repo;

        public TaxResidencyController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateTaxResidency([FromBody] TaxResidencyDto dto)
        {
            try
            {
                bool taxResidencyExists = _repo.TaxResidency.DoesTaxResidencyExist(dto);                

                if (taxResidencyExists)
                {
                    return BadRequest("Tax Residency Exists");
                }
                else 
                { 
                     _repo.TaxResidency.CreateTaxResidency(dto);
                }
                return Ok("Tax Residency Created");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateTaxResidency([FromBody] TaxResidencyDto dto)
        {
            try
            {
                bool taxResidencyExists = _repo.TaxResidency.DoesTaxResidencyExist(dto);               

                if (!taxResidencyExists)
                {
                    CreateTaxResidency(dto);
                }
                else 
                { 
                    _repo.TaxResidency.UpdateTaxResidency(dto);
                }

                return Ok("Tax Residency Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetTaxResidency(int clientId)
        {
            try
            {
                TaxResidencyDto taxResidency = _repo.TaxResidency.GetTaxResidency(clientId);

                return Ok(taxResidency);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, AllowAnonymous]
        public IActionResult DeleteTaxResidencyItem(int id)
        {
            try
            {
                bool deleted = _repo.TaxResidency.DeleteTaxResidencyItem(id);

                return Ok(deleted);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}