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
        #region Private Fields

        private readonly IWrapper _repo;

        #endregion Private Fields

        #region Public Constructors

        public RetirementPlanningController(IWrapper repo)
        {
            _repo = repo;
        }

        #endregion Public Constructors


        #region Public Methods

        [HttpPost, AllowAnonymous]
        public IActionResult CreateRetirementPlanning([FromBody] RetirementPlanningDto dto)
        {
            try
            {
                bool retirementPlanningExists = _repo.RetirementPlanning.DoesRetirementPlanningExist(dto);                

                if (retirementPlanningExists)
                {
                    return BadRequest("Retirement Planning Exists");
                }
                else
                { 
                    _repo.RetirementPlanning.CreateRetirementPlanning(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetRetirementPlanning(int clientId)
        {
            try
            {
                RetirementPlanningDto dto = _repo.RetirementPlanning.GetRetirementPlanning(clientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
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
                    CreateRetirementPlanning(dto);
                }
                else
                {
                    _repo.RetirementPlanning.UpdateRetirementPlanning(dto);
                }

                return Ok("Retirement Planning Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion Public Methods
    }
}