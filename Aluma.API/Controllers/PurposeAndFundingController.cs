using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class PurposeAndFundingController : ControllerBase
    {
        private readonly IWrapper _repo;

        public PurposeAndFundingController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreatePurposeAndFunding([FromBody] PurposeAndFundingDto dto)
        {
            try
            {
                bool purposeAndFundingExist = _repo.PurposeAndFunding.DoesPurposeAndFundingExist(dto);

                if (purposeAndFundingExist)
                {
                    return BadRequest("Purpose And Funding Exist");
                }
                else
                {
                    _repo.PurposeAndFunding.CreatePurposeAndFunding(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdatePurposeAndFunding([FromBody] PurposeAndFundingDto dto)
        {
            bool purposeAndFundingExist = _repo.PurposeAndFunding.DoesPurposeAndFundingExist(dto);

            if (!purposeAndFundingExist)
            {
                CreatePurposeAndFunding(dto);
            }
            else
            {
                _repo.PurposeAndFunding.UpdatePurposeAndFunding(dto);
            }
            return Ok(dto);
        }


        [HttpGet, AllowAnonymous]
        public IActionResult GetPurposeAndFunding(int applicationId)
        {
            try
            {
                PurposeAndFundingDto purposeAndFunding = _repo.PurposeAndFunding.GetPurposeAndFunding(applicationId);

                return Ok(purposeAndFunding);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }




    }
}