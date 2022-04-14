using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IConfiguration _config;

        public OTPController(IWrapper repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("verify/login")]
        public IActionResult VerifyLoginOtp(LoginDto dto)
        {
            UserDto user = _repo.User.GetUser(dto);
            AuthResponseDto response = new AuthResponseDto();
            string isOtpVerified = _repo.Otp.VerifyOTP(dto.Otp, user.Id);

            if (isOtpVerified != "Validated")
            {
                response.Message = "Invalid OTP";
                return StatusCode(401, response);
            }

            //Todo: lots of duplicated code, fix in next sprint. -

            RoleEnum role = user.Role;
            var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
            string token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);
            if (role == RoleEnum.Client)
            {
                ClientDto client = _repo.Client.GetClientByUserId(user.Id);
                response.ClientId = client.Id;
            }
            else if (role == RoleEnum.Advisor || role == RoleEnum.Admin)
            {
                AdvisorDto advisor = _repo.Advisor.GetAdvisorByUserId(user.Id);
                response.AdvisorId = advisor.Id;
            }

            response.Message = "OtpVerified";
            response.Token = token;
            response.TokenExpiry = DateTime.Now.AddMinutes(jwtSettings.LifeSpan).ToString();
            response.User = user;

            return Ok(response);
        }

        [HttpPost("verify/register")]
        public IActionResult VerifyRegisterOtp(LoginDto dto)
        {
            UserDto user = _repo.User.GetUser(dto);
            AuthResponseDto response = new AuthResponseDto();

            string isOtpVerified = _repo.Otp.VerifyOTP(dto.Otp, user.Id);

            if (isOtpVerified != "Validated")
            {
                response.Message = "Invalid OTP";
                return StatusCode(401, response);
            }

            _repo.User.VerifyUser(user);

            RoleEnum role = user.Role;
            var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
            string token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);
            if (role == RoleEnum.Client)
            {
                ClientDto client = _repo.Client.GetClientByUserId(user.Id);
                response.ClientId = client.Id;
            }
            else if (role == RoleEnum.Advisor)
            {
                AdvisorDto advisor = _repo.Advisor.GetAdvisorByUserId(user.Id);
                response.AdvisorId = advisor.Id;
            }

            response.Message = "OtpVerified";
            response.Token = token;
            response.TokenExpiry = DateTime.Now.AddMinutes(jwtSettings.LifeSpan).ToString();
            response.User = user;
           

            return Ok(response);
        }

        [HttpGet("verify/signature")]
        public IActionResult VerifySignatureOtp(int applicationId, string otp)
        {
            UserDto user = _repo.User.GetUserByApplicationID(applicationId);
            AuthResponseDto response = new AuthResponseDto();

            string isOtpVerified = _repo.Otp.VerifyOTP(otp, user.Id, applicationId);

            if (isOtpVerified != "Validated")
            {
                return StatusCode(401, "Invalid OTP");
            }

            response.Message = "SignatureVerified";
            
            _repo.Applications.SignDocuments(applicationId);

            return Ok(response);
        }


        //[HttpPost("resend/otp/{email}"), AllowAnonymous]
        //public IActionResult ResendOTP(string email)
        //{
        //    try
        //    {
        //        var checkUser = _repo.User.FindByCondition(c => (c.Email == email));

        //        // check that there are no users with this email
        //        if (checkUser.Any())
        //        {
        //            if (checkUser.FirstOrDefault().MobileVerified)
        //            {
        //                return StatusCode(403, "This account is already verified,  please sign in.");
        //            }
        //            else
        //            {
        //                string sentOTP = _repo.User.CreateOTP(checkUser.FirstOrDefault(), OtpTypesEnum.Registration);

        //                if (sentOTP == "Success")
        //                {
        //                    _repo.Save();
        //                    return StatusCode(201);
        //                }
        //                else
        //                {
        //                    return StatusCode(403, sentOTP);
        //                }

        //            }
        //        }
        //        else
        //        {
        //            return StatusCode(403, "Invalid account credentials.");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
    }
}