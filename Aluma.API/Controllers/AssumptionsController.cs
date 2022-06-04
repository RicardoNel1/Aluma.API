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
                    dto.Status = "Server Error";
                    dto.Message = "Assumptions Exists";
                    return BadRequest(dto);
                }
                else
                {
                    dto = _repo.Assumptions.CreateAssumptions(dto);

                    dto.Status = "Successful";
                    dto.Message = "Assumptions Created";

                    if (dto.Status.ToLower().Contains("success"))
                    {
                        return Ok(dto);
                    }
                    else
                    {
                        return BadRequest(dto);
                    }
                }
            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
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
                    return CreateAssumptions(dto);
                }
                else
                {
                    dto = _repo.Assumptions.UpdateAssumptions(dto);
                    if (dto.Status.ToLower().Contains("success"))
                    {
                        return Ok(dto);
                    }
                    else
                    {
                        return BadRequest(dto);
                    }
                }

            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
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