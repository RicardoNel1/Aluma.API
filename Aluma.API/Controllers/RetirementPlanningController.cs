using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class RetirementPlanningController : ControllerBase
    {
        private readonly IWrapper _repo;

        public RetirementPlanningController(IWrapper repo)
        {
            _repo = repo;
        }


        [HttpPost, AllowAnonymous]
        public IActionResult CreateRetirementPlanning([FromBody] RetirementPlanningDto dto)
        {
            try
            {
                bool retirementPlanningExists = _repo.RetirementPlanning.DoesRetirementPlanningExist(dto);                

                if (retirementPlanningExists)
                {
                    dto.Status = "Failure";
                    dto.Message = "Retirement Planning Exists";
                    return BadRequest(dto);
                }
                else
                { 
                    dto = _repo.RetirementPlanning.CreateRetirementPlanning(dto);
                    dto.Status = "Success";
                }
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
        public IActionResult UpdateRetirementPlanning([FromBody] RetirementPlanningDto dto)
        {          
            try
            {
                bool retirementPlanningExist = _repo.RetirementPlanning.DoesRetirementPlanningExist(dto);

                if (!retirementPlanningExist)
                {
                    dto = _repo.RetirementPlanning.CreateRetirementPlanning(dto);
                    dto.Status = "Success";                
                }
                else
                {
                    dto = _repo.RetirementPlanning.UpdateRetirementPlanning(dto);
                    dto.Status = "Success";
                }

                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetRetirementPlanning(int fnaId)
        {
            try
            {
                RetirementPlanningDto dto = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);

                if (dto == null)
                {
                    dto = new() {
                        FNAId= fnaId,
                        Status= "Bad Request",
                        Message = $"Retirement Planning data is not available for fna id: {fnaId}"
                    };

                    return BadRequest(dto);
                }

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}