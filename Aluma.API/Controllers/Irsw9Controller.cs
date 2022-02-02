using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class Irsw9Controller : ControllerBase
    {
        private readonly IWrapper _repo;

        public Irsw9Controller(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult CreateIRSW9([FromBody] IRSW9Dto dto)
        {
            try
            {
                bool irswExist = _repo.IRSW9.DoesApplicationHaveIRSW9(dto);
                if (irswExist)
                {
                    return BadRequest("IRSW9 Exists");
                }

                IRSW9Dto irsw = _repo.IRSW9.CreateIRSW9(dto);

                return Ok(irsw);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateIRSW9([FromBody] IRSW9Dto dto)
        {
            try
            {
                bool irswExist = _repo.IRSW9.DoesApplicationHaveIRSW9(dto);
                if (!irswExist)
                {
                    return BadRequest("IRSW9 Does Not Exist");
                }

                IRSW9Dto irsw = _repo.IRSW9.UpdateIRSW9(dto);

                return Ok(irsw);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetIRSW9([FromBody] IRSW9Dto dto)
        {
            try
            {
                IRSW9Dto irsw = _repo.IRSW9.GetIRSW9(dto);

                return Ok(irsw);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteIRSW9(IRSW9Dto dto)
        {
            try
            {
                bool isDeleted = _repo.IRSW9.DeleteIRSW9(dto);
                if (!isDeleted)
                {
                    return BadRequest("IRSW9 Not Deleted");
                }
                return Ok("IRSW9 Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}