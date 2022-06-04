using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class AssumptionsController : ControllerBase
    {
        private readonly IWrapper _repo;

        public AssumptionsController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateAssumptions([FromBody] AssumptionsDto dto)
        {
            try
            {
                bool assumptionsExist = _repo.Assumptions.DoesAssumptionsExist(dto);                

                if (assumptionsExist)
                {
                    return BadRequest("Assumptions Exists");
                }
                else
                { 
                    _repo.Assumptions.CreateAssumptions(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateAssumptions([FromBody] AssumptionsDto dto)
        //public IActionResult UpdateAssumptions([FromBody] AssumptionsDto dto)
        {
            try
            {
                bool assumptionsExist = _repo.Assumptions.DoesAssumptionsExist(dto);

                if (!assumptionsExist)
                {
                    CreateAssumptions(dto);
                }
                else
                {
                    _repo.Assumptions.UpdateAssumptions(dto);
                }

                return Ok("Assumptions Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetAssumptions(int fnaId)
        {
            try
            {
                AssumptionsDto dto = _repo.Assumptions.GetAssumptions(fnaId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
                

        

    }
}