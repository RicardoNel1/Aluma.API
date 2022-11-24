using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Advisor,Admin")]
    public class FSASController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IMapper _mapper;

        public FSASController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SubmitCCP(ClientDto dto)
        {
            try
            {
                var ccp = _repo.FSASRepo.SubmitClientCCPRequest(dto);
                return Ok(ccp);
                
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateAdvisor(AdvisorDto dto)
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
                    return Ok(advisor);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetFSASInformation(int clientId)
        {
            try
            {
                var ccp = _repo.FSASRepo.GetClientCCP(clientId);

                return Ok(ccp);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("claim"), AllowAnonymous]
        public IActionResult getClaim()
        {
            try
            {
                var claims = _repo.JwtRepo.IsTokenValid(Request.Headers[HeaderNames.Authorization].ToString());//.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                return Ok(claims);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list")]
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