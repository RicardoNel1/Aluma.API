using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using DataService.Dto.Advisor;
using DataService.Dto.Client;
using DataService.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Advisor,Admin")]
    public class AdvisorController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AdvisorController(IWrapper repo, IMapper mapper, IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateAdvisor(AdvisorDto dto)
        {
            try
            {
                AuthResponseDto response = new();
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
                        response.Message = "InvalidID";
                        return StatusCode(403, response);
                    }

                    var advisor = await _repo.Advisor.CreateAdvisor(dto);
                    return Ok(advisor);
                }
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

        [HttpGet("astute"), AllowAnonymous]
        public IActionResult GetAdvisorAstute(int advisorId)
        {
            try
            {
                var advisor = _repo.Advisor.GetAdvisorAstute(new AdvisorAstuteDto() { AdvisorId = advisorId });

                if (advisor == null)
                {
                    return Ok(new AdvisorAstuteDto());
                }
                else
                {
                    return Ok(advisor);
                }
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

        [HttpPost("astute"), AllowAnonymous]
        public async Task<IActionResult> CreateAdvisorAstute(AdvisorAstuteDto dto)
        {
            try
            {
                var advisor = await _repo.Advisor.CreateAdvisorAstute(dto);
                return Ok(advisor);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("astute"), AllowAnonymous]
        public IActionResult UpdateAstuteAdvisor(AdvisorAstuteDto dto)
        {
            UserDto AstuteUserDto = new UserDto { Id = dto.AdvisorId };
            AstuteUserDto = _repo.User.GetUser(AstuteUserDto);
            try
            {
                bool advisorAstuteExists = _repo.Advisor.DoesAdvisorExist(AstuteUserDto);
                if (!advisorAstuteExists)
                {
                    return BadRequest("Astute Advisor Does Not Exist");
                }
                else
                {
                    var advisorAstute = _repo.Advisor.UpdateAdvisorAstute(dto);
                    return Ok(advisorAstute);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("astute-cred"), AllowAnonymous]
        public IActionResult GetAstuteAdvisorCredential(int advisorId)
        {
            try
            {
                var advisor = _repo.Advisor.GetAstuteAdvisorCredential(advisorId);

                if (advisor == null)
                {
                    return Ok(new AdvisorAstuteDto());
                }
                else
                {
                    return Ok(advisor);
                }

                return Ok(advisor);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }




    }
}