using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class Irsw8Controller : ControllerBase
    {
        private readonly IWrapper _repo;

        public Irsw8Controller(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult CreateIRSW8([FromBody] IRSW8Dto dto)
        {
            try
            {
                bool irswExist = _repo.IRSW8.DoesApplicationHaveIRSW8(dto);
                if (irswExist)
                {
                    return BadRequest("IRSW8 Exists");
                }

                IRSW8Dto irsw = _repo.IRSW8.CreateIRSW8(dto);

                return Ok(irsw);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateIRSW8([FromBody] IRSW8Dto dto)
        {
            try
            {
                bool irswExist = _repo.IRSW8.DoesApplicationHaveIRSW8(dto);
                if (!irswExist)
                {
                    return BadRequest("IRSW8 Does Not Exist");
                }

                IRSW8Dto irsw = _repo.IRSW8.UpdateIRSW8(dto);

                return Ok(irsw);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetIRSW8([FromBody] IRSW8Dto dto)
        {
            try
            {
                IRSW8Dto irsw = _repo.IRSW8.GetIRSW8(dto);

                return Ok(irsw);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteIRSW8(IRSW8Dto dto)
        {
            try
            {
                bool isDeleted = _repo.IRSW8.DeleteIRSW8(dto);
                if (!isDeleted)
                {
                    return BadRequest("IRSW8 Not Deleted");
                }
                return Ok("IRSW8 Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}