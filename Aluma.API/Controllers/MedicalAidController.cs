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
    public class MedicalAidController : Controller
    {
        private readonly IWrapper _repo;

        public MedicalAidController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetShortTerm(int clientId)
        {
            try
            {
                MedicalAidDTO sortTermInsuranceDTOs = _repo.MedicalAid.GetMedicalAid(clientId);
                return Ok(sortTermInsuranceDTOs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateShortTerm([FromBody] MedicalAidDTO dto)
        {
            try
            {
                dto = _repo.MedicalAid.UpdateMedicalAid(dto);

                if (dto.Status != "Success" && !string.IsNullOrEmpty(dto.Status))
                    return BadRequest(dto);

                return Ok(dto);
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
                bool deleted = _repo.MedicalAid.DeleteMedicalAid(id);
                
                if (deleted)
                    return NoContent();
                
                return BadRequest("Medical Aid could not be deleted.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}