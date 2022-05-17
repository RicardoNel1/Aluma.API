using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class TaxLumpsumController : ControllerBase
    {
        private readonly IWrapper _repo;

        public TaxLumpsumController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetTaxLumpsum(int fnaId)
        {
            try
            {
                TaxLumpsumDto taxLumpsum = _repo.TaxLumpsum.GetTaxLumpsum(fnaId);
                return Ok(taxLumpsum);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTaxLumpsum(TaxLumpsumDto dto)
        {
            try
            {
                TaxLumpsumDto result = _repo.TaxLumpsum.CreateTaxLumpsum(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateTaxLumpsum(TaxLumpsumDto dto)
        {
            try
            {
                TaxLumpsumDto result = _repo.TaxLumpsum.CreateTaxLumpsum(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteTaxLumpsum(int clientId)
        {
            return null;
        }


    }
}
