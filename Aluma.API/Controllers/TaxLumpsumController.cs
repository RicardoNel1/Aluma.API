using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]")]
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

        [HttpPost, AllowAnonymous]
        public IActionResult CreateTaxLumpsum([FromBody] TaxLumpsumDto dto)
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

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateTaxLumpsum(TaxLumpsumDto dto)
        {
            try
            {
                TaxLumpsumDto result = _repo.TaxLumpsum.UpdateTaxLumpsum(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[HttpDelete]
        //public IActionResult DeleteTaxLumpsum(int clientId)
        //{
        //    return null;
        //}


    }
}
