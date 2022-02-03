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
                //bool taxResidencyExists = _repo.TaxResidency.DoesTaxResidencyExist(dto);
                bool taxResidencyExists = false;        //

                if (taxResidencyExists)
                {
                    return BadRequest("Tax Residency Exists");
                }

                TaxResidencyDto taxResidency = _repo.TaxResidency.CreateTaxResidency(dto);

                return Ok(taxResidency);
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
                //bool taxResidencyExists = _repo.TaxResidency.DoesTaxResidencyExist(dto);
                bool taxResidencyExists = false;        //remove
                if (!taxResidencyExists)
                {
                    CreateTaxResidency(dto);
                }

                TaxResidencyDto taxResidency = _repo.TaxResidency.UpdateTaxResidency(dto);

                return Ok(taxResidency);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        //public IActionResult GetClientBankDetails([FromBody] ClientDto dto)
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

    }
}