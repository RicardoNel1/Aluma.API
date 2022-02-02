﻿using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class FSPMandateController : ControllerBase
    {
        private readonly IWrapper _repo;

        public FSPMandateController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult CreateMandate([FromBody] FSPMandateDto dto)
        {
            try
            {
                bool mandateExist = _repo.FSPMandate.DoesApplicationHaveMandate(dto);
                if (mandateExist)
                {
                    return BadRequest("FSP Exists");
                }

                FSPMandateDto mandate = _repo.FSPMandate.CreateFSPMandate(dto);

                return Ok(mandate);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateMandate([FromBody] FSPMandateDto dto)
        {
            try
            {
                bool mandateExist = _repo.FSPMandate.DoesApplicationHaveMandate(dto);
                if (mandateExist)
                {
                    return BadRequest("FSP Does Not Exist");
                }

                FSPMandateDto mandate = _repo.FSPMandate.UpdateFSPMandate(dto);

                return Ok(mandate);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetMandate([FromBody] FSPMandateDto dto)
        {
            try
            {
                FSPMandateDto mandate = _repo.FSPMandate.GetFSPMandate(dto);

                return Ok(mandate);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteMandate(FSPMandateDto dto)
        {
            try
            {
                bool isDeleted = _repo.FSPMandate.DeleteFSPMandate(dto);
                if (!isDeleted)
                {
                    return BadRequest("Mandate Not Deleted");
                }
                return Ok("Mandate Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}