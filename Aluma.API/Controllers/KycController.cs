using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class KycController : ControllerBase
    {
        #region Private Fields

        private readonly IWrapper _repo;

        #endregion Private Fields

        #region Public Constructors

        public KycController(IWrapper repo)
        {
            _repo = repo;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public IActionResult CreateKycEvent([FromBody] ClientDto dto)
        {
            try
            {
                bool kycExists = _repo.KycData.DoesClientHaveKYC(dto);
                if (kycExists)
                {
                    return BadRequest("KYC Exists");
                }
                else
                {
                    _repo.KycData.CreateClientKycEvent(dto);
                }
                return Ok("KYC Event Created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteKycEvent(ClientDto dto)
        {
            try
            {
                bool isDeleted = _repo.KycData.DeleteKycEvent(dto);
                if (!isDeleted)
                {
                    return BadRequest("Kyc Not Deleted");
                }
                return Ok("Kyc Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetKycEvent([FromBody] ClientDto dto)
        {
            try
            {
                KycDataDto kyc = _repo.KycData.GetClientKycEvent(dto);

                return Ok(kyc);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list/admin"), Authorize(Roles = "Admin")]
        public IActionResult ListAllKycApplications()
        {
            try
            {
                var kycList = _repo.KycData.GetAllKycEvents();

                return Ok(kycList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateKycEvent([FromBody] ClientDto dto)
        {
            try
            {
                bool kycExists = _repo.KycData.DoesClientHaveKYC(dto);
                if (!kycExists)
                {
                    return BadRequest("KYC Doesn't Exist");
                }
                else
                {
                    _repo.KycData.UpdateClientKycEvent(dto);
                }
                return Ok("KYC Event Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion Public Methods
    }
}