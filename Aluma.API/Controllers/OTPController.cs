using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

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
            AuthResponseDto response = new();
            string isOtpVerified = _repo.Otp.VerifyOTP(dto.Otp, user.Id);

            if (isOtpVerified != "Validated")
            {
                response.Message = "Invalid OTP";
                return StatusCode(401, response);
            }

            RoleEnum role = user.Role;
            var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
            string token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);
            if (role == RoleEnum.Client)
            {
                ClientDto client = _repo.Client.GetClientByUserId(user.Id);
                response.Client = client;
            }
            else if (role == RoleEnum.Advisor || role == RoleEnum.Admin)
            {
                AdvisorDto advisor = _repo.Advisor.GetAdvisorByUserId(user.Id);
                response.AdvisorId = advisor.Id;
                user.HasSignature = user.Signature != null && user.Signature.Length > 0;
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
            AuthResponseDto response = new();

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
                response.Client = client;
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

        [HttpPost("verify/reset-password")]
        public IActionResult VerifyResetPasswordOtp(ResetPasswordDto dto)
        {

            AuthResponseDto response = new();
            UserDto user = new();

            if (dto.UserName != "" && dto.UserName != null)
            {
                LoginDto login = new()
                {
                    UserName = dto.UserName,
                    Password = dto.Password
                };

                if (_repo.User.DoesUserNameExist(login))
                {
                    user = _repo.User.GetUser(login);
                }
                else
                {
                    response.Message = "Invalid-NotExists";
                    return StatusCode(401, response);
                }
            }
            else
            {
                int userId = _repo.User.DecryptUserId(dto.UserId);
                user.Id = userId;
                user = _repo.User.GetUser(user);
            }

            string isOtpVerified = _repo.Otp.VerifyOTP(dto.Otp, user.Id);

            if (isOtpVerified != "Validated")
            {
                response.Message = "Invalid OTP";
                return StatusCode(401, response);
            }


            if (user.Role == RoleEnum.Admin || user.Role == RoleEnum.Advisor || user.Role == RoleEnum.External)
            {
                _repo.User.VerifyUser(user);
            }

            _repo.User.ResetPassword(user.Id, dto.Password);

            RoleEnum role = user.Role;

            if (role == RoleEnum.Client)
            {
                ClientDto client = _repo.Client.GetClientByUserId(user.Id);
                response.Client = client;
            }
            else if (role == RoleEnum.Advisor)
            {
                AdvisorDto advisor = _repo.Advisor.GetAdvisorByUserId(user.Id);
                response.AdvisorId = advisor.Id;
            }
            response.Status = "Success";
            response.Message = "OtpVerified";

            return Ok(response);
        }

        [HttpGet("verify/signature")]
        public async Task<IActionResult> VerifySignatureOtp(int applicationId, string otp)
        {

            AuthResponseDto response = new();

            try
            {
                UserDto user = _repo.User.GetUserByApplicationID(applicationId);

                string isOtpVerified = _repo.Otp.VerifyOTP(otp, user.Id, applicationId);

                if (isOtpVerified != "Validated")
                {
                    return StatusCode(401, "Invalid OTP");
                }

                response.Message = "SignatureVerified";

                await _repo.SignHelper.SignDocuments(applicationId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                response.Message = "InternalError";
                return StatusCode(500, response); ;
            }
        }

        [HttpPost("verify/consent")]
        public IActionResult VerifyConsentOtp(LoginDto dto)
        {
            UserDto user = _repo.User.GetUser(dto);
            AuthResponseDto response = new();
            string isOtpVerified = _repo.Otp.VerifyOTP(dto.Otp, user.Id);

            if (isOtpVerified != "Validated")
            {
                response.Message = "Invalid OTP";
                return StatusCode(401, response);
            }

            RoleEnum role = user.Role;
            if (role == RoleEnum.Client)
            {
                ClientDto client = _repo.Client.GetClientByUserId(user.Id);
                response.Client = client;
            }
            else if (role == RoleEnum.Advisor || role == RoleEnum.Admin)
            {
                AdvisorDto advisor = _repo.Advisor.GetAdvisorByUserId(user.Id);
                response.AdvisorId = advisor.Id;
                user.HasSignature = user.Signature != null && user.Signature.Length > 0;
            }

            response.Message = "OtpVerified";
            response.User = user;

            return Ok(response);
        }
    }
}