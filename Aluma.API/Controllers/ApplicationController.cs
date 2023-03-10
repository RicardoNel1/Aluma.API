using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using DataService.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IMapper _mapper;

        public ApplicationController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateApplication(ApplicationDto dto)
        {
            try
            {
                bool applicationStarted = _repo.Applications.ApplicationInProgress(dto);
                if (!applicationStarted)
                {
                    var result = _repo.Applications.CreateNewApplication(dto);
                    return Ok(result);
                }
                else
                {
                    var result = _repo.Applications.GetCurrentApplication(dto);
                    return Ok(result);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("submitShortApplication"), AllowAnonymous]
        public IActionResult SubmitShortApplication(ApplicationDto dto)
        {
            try
            {
                dto = _repo.Applications.SubmitShortApplication(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("amount"), AllowAnonymous]
        public IActionResult SetApplicationAmount(ApplicationDto dto)
        {
            try
            {
                bool applicationExist = _repo.Applications.DoesApplicationExist(dto);
                if (!applicationExist)
                {
                    return BadRequest("Application Does Not Exist");
                }
                else
                {
                    _repo.Applications.SetApplicationAmount(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[HttpGet("current"), AllowAnonymous]
        //public IActionResult GetCurrentApplication(int clientId, string selectedProduct, string applicationStatus )
        //{
        //    try
        //    {              
        //        var application = _repo.Applications.GetCurrentApplication(clientId, selectedProduct, applicationStatus);

        //        return Ok(application);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        [HttpPut("delete"), AllowAnonymous]
        public IActionResult SoftDeleteApplication(ApplicationDto dto)
        {
            try
            {

                bool applicationExist = _repo.Applications.DoesApplicationExist(dto);
                if (!applicationExist)
                {
                    return BadRequest("Application Does Not Exist");
                }
                else
                {
                    _repo.Applications.SoftDeleteApplication(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateApplication([FromBody] ApplicationDto dto)
        {
            try
            {
                var application = _repo.Applications.UpdateApplication(dto);

                return Ok(application);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetApplication(int applicationId)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                var application = _repo.Applications.GetApplication(new ApplicationDto() { Id = applicationId });

                return Ok(application);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteApplication(ApplicationDto dto)
        {
            try
            {
                bool isDeleted = _repo.Applications.DeleteApplication(dto);
                if (!isDeleted)
                {
                    return BadRequest("Application Not Deleted");
                }
                return Ok("Application Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list/client"), AllowAnonymous]
        //public IActionResult ListClientApplication([FromQuery] ClientDto dto) 
        public IActionResult ListClientApplications(int clientId)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                //var applications = _repo.Applications.GetApplicationsByClient(dto);
                var applications = _repo.Applications.GetApplicationsByClient(clientId);

                return Ok(applications);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("exist"), AllowAnonymous]
        public IActionResult DoesApplicationsExist(int clientId)
        {
            try
            {
                //var applications = _repo.Applications.GetApplicationsByClient(clientId);

                //return Ok(applications);
                bool applicationExist = _repo.Applications.DoesApplicationExist(clientId);
                if (!applicationExist)
                {
                    return Ok(false);
                }
                else
                {
                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("documents"), AllowAnonymous]
        public async Task<IActionResult> DownloadAllApplicationDocuments(int applicationId)
        {
            try
            {
                List<ApplicationDocumentDto> appDocs = await _repo.Applications.GetApplicationDocuments(applicationId);

                return Ok(appDocs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("createDocuments"), AllowAnonymous]
        public async Task<IActionResult> GenerateDocuments(int applicationId)
        {
            try
            {
                bool applicationExist = _repo.Applications.DoesApplicationExist(new ApplicationDto() { Id = applicationId });

                if (!applicationExist)
                {
                    return BadRequest("Application does not exist");
                }
                else
                {
                    await _repo.Applications.GenerateApplicationDocuments(applicationId);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("signDocuments"), AllowAnonymous]
        public async Task<IActionResult> SignDocuments(int applicationId)
        {
            try
            {
                AuthResponseDto response = new();
                ApplicationDto dto = new() { Id = applicationId };


                bool applicationExist = _repo.Applications.DoesApplicationExist(dto);

                if (!applicationExist)
                {
                    return BadRequest("Application does not exist");
                }
                else
                {
                    if (_repo.Applications.CheckSignConsent(applicationId))
                    {
                        response.Message = "consentedSignature";

                        await _repo.SignHelper.SignDocuments(applicationId);
                    }
                    else
                    {
                        _repo.Applications.ConsentToSign(applicationId);
                        UserDto user = _repo.User.GetUserByApplicationID(applicationId);
                        _repo.Otp.SendOTP(user, OtpTypesEnum.SignDocument, applicationId);
                        response.Message = "verifySignature";
                    }

                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list/advisor"), Authorize(Roles = "Advisor,Admin")]
        public IActionResult ListAdvisorApplications(AdvisorDto dto)
        {
            try
            {
                var applicationList = _repo.Applications.GetApplicationsByAdvisor(dto);

                return Ok(applicationList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list/admin"), Authorize(Roles = "Admin")]
        public IActionResult ListAllApplications()
        {
            try
            {
                var applicationList = _repo.Applications.GetApplications();

                return Ok(applicationList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}