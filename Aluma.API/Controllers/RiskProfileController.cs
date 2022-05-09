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
        #region Private Fields

        private readonly IWrapper _repo;

        #endregion Private Fields

        #region Public Constructors

        public RiskProfileController(IWrapper repo)
        {
            _repo = repo;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost, AllowAnonymous]
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

        [HttpGet("clientId"), AllowAnonymous]
        public IActionResult GetRiskProfile(int clientId)
        {
            RiskProfileDto response = null;
            try
            {
                //ClientDto client = _repo.Client.GetClient(userId);
                response = _repo.RiskProfile.GetRiskProfile(clientId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, response);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateRiskProfile([FromBody] RiskProfileDto dto)
        {
            try
            {
                bool riskExists = _repo.RiskProfile.DoesClientHaveRiskProfile(dto);
                if (!riskExists)
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

        #endregion Public Methods
    }
}