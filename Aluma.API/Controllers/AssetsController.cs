using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IWrapper _repo;

        public AssetsController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost("primary_residence"), AllowAnonymous]
        public IActionResult CreatePrimaryResidence([FromBody] PrimaryResidenceDto dto)
        {
            try
            {
                bool primaryResidenceExists = _repo.PrimaryResidence.DoesPrimaryResidenceExist(dto);                

                if (primaryResidenceExists)
                {
                    return BadRequest("Primary Residence Exists");
                }
                else
                { 
                    _repo.PrimaryResidence.CreatePrimaryResidence(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("primary_residence"), AllowAnonymous]
        public IActionResult UpdatePrimaryResidence([FromBody] PrimaryResidenceDto dto)
        {          
            try
            {
                bool primaryResidenceExist = _repo.PrimaryResidence.DoesPrimaryResidenceExist(dto);

                if (!primaryResidenceExist)
                {
                    CreatePrimaryResidence(dto);
                }
                else
                {
                    _repo.PrimaryResidence.UpdatePrimaryResidence(dto);
                }

                return Ok("Primary Residence Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("primary_residence"), AllowAnonymous]
        public IActionResult GetPrimaryResidence(int clientId)
        {
            try
            {
                PrimaryResidenceDto dto = _repo.PrimaryResidence.GetPrimaryResidence(clientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}