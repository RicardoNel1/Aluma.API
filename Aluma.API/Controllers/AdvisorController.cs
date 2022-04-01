using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Admin,Broker")]
    public class AdvisorController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IMapper _mapper;

        public AdvisorController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateAdvisor([FromBody] AdvisorDto dto)
        {
            try
            {
                bool advisorExists = _repo.Advisor.DoesAdvisorExist(dto.User);
                if (advisorExists)
                {
                    return BadRequest("Advisor Exists");
                }
                else
                {
                    bool checkID = _repo.User.ValidateID(dto.User.RSAIdNumber);
                    // check if valid id number has been entered
                    if (checkID == false)
                    {
                        return StatusCode(403, "Invalid ID");
                    }

                    var advisor = _repo.Advisor.CreateAdvisor(dto);
                    return Ok("Advisor Created");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateAdvisor([FromBody] AdvisorDto dto)
        {
            try
            {
                bool advisorExists = _repo.Advisor.DoesAdvisorExist(dto.User);
                if (!advisorExists)
                {
                    return BadRequest("Advisor Does Not Exist");
                }
                else
                {
                    var advisor = _repo.Advisor.UpdateAdvisor(dto);
                    return Ok("Advisor Updated");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetAdvisor(int advisorId)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                var advisor = _repo.Advisor.GetAdvisor(new AdvisorDto() { Id = advisorId });

                return Ok(advisor);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list"), AllowAnonymous]
        public IActionResult GetAllAdvisors()
        {
            try
            {
                var advisor = _repo.Advisor.GetAllAdvisors();

                return Ok(advisor);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteAdvisor(AdvisorDto dto)
        {
            try
            {
                bool isDeleted = _repo.Advisor.DeleteAdvisor(dto);
                if (!isDeleted)
                {
                    return BadRequest("Advisor Not Deleted");
                }
                return Ok("Advisor Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}