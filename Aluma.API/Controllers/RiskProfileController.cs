using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class RiskProfileController : ControllerBase
    {
        private readonly IWrapper _repo;

        public RiskProfileController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult CreateRiskProfile([FromBody] RiskProfileDto dto)
        {
            try
            {
                bool riskExists = _repo.RiskProfile.DoesClientHaveRiskProfile(dto);
                if (riskExists)
                {
                    return BadRequest("Risk Profile Exists");
                }

                RiskProfileDto risk = _repo.RiskProfile.CreateRiskProfile(dto);

                return Ok(risk);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateRiskProfile([FromBody] RiskProfileDto dto)
        {
            try
            {
                bool riskExists = _repo.RiskProfile.DoesClientHaveRiskProfile(dto);
                if (riskExists)
                {
                    return BadRequest("Risk Profile Does Not Exist");
                }

                RiskProfileDto risk = _repo.RiskProfile.UpdateRiskProfile(dto);

                return Ok(risk);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("userId")]
        public IActionResult GetRiskProfile(int userId)
        {
            RiskProfileDto response = null;
            try
            {
                ClientDto client = _repo.Client.GetClient(userId);
                response = _repo.RiskProfile.GetRiskProfile(client.Id);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, response);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteRiskProfile(RiskProfileDto dto)
        {
            try
            {
                bool isDeleted = _repo.RiskProfile.DeleteRiskProfile(dto);
                if (!isDeleted)
                {
                    return BadRequest("RiskProfile Not Deleted");
                }
                return Ok("RiskProfile Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}