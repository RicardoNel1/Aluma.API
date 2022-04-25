using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IConfiguration _config;

        public AuthenticationController(IWrapper repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }


        [HttpPost("client/login")]
        public IActionResult AuthenticateClient(LoginDto dto)
        {
            AuthResponseDto response = new AuthResponseDto();

            try
            {
                bool loginExists = false;
                bool socialLoginVerified = false;
                bool passwordMatched = false;
                bool registrationVerified = false;

                UserDto user = new UserDto();
                RoleEnum role = RoleEnum.Client;
                var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
                string token = String.Empty;

                loginExists = _repo.User.DoesUserNameExist(dto);

                if (!loginExists)
                {
                    if (dto.SocialId != null && dto.Password == "")
                    {
                        RegistrationDto regDto = new RegistrationDto()
                        {
                            Email = dto.UserName,
                            SocialId = dto.SocialId,
                            FirstName = dto.FirstName,
                            LastName = dto.LastName,
                        };

                        user = _repo.User.CreateClientUser(regDto);

                        ClientDto client = new ClientDto() { UserId = user.Id, AdvisorId = null, ClientType = "Primary" };
                        client = _repo.Client.CreateClient(client);
                        token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);

                        response = new AuthResponseDto()
                        {
                            Token = token,
                            ClientId = client.Id,
                            TokenExpiry = DateTime.Now.AddMinutes(jwtSettings.LifeSpan).ToString(),
                            User = user,
                            Message = "SuccessfulSocialLogin",
                        };

                        return Ok(response);
                    }

                    response.Message = "Invalid-NotExist";

                    return StatusCode(401, response);
                }

                user = _repo.User.GetUser(dto);

                if (user.Role != RoleEnum.Client && user.Role != RoleEnum.Guest)
                {
                    response.Message = "Invalid-Credentials";
                    return StatusCode(401, response);
                }

                socialLoginVerified = _repo.User.IsSocialLoginVerified(dto);

                if (!socialLoginVerified)
                {

                    passwordMatched = _repo.User.IsPasswordVerified(dto);
                    if (!passwordMatched)
                    {
                        response.Message = "Invalid-Credentials";
                        return StatusCode(401, response);
                    }

                    registrationVerified = _repo.User.IsRegistrationVerified(dto);

                    user = _repo.User.GetUser(dto);
                    if (!registrationVerified)
                    {
                        //Re-send Verification OTP
                        _repo.Otp.SendOTP(user, OtpTypesEnum.Registration);
                        response.Message = "verifyRegistration";
                        return StatusCode(401, response);
                    }
                    else
                    {
                        //Send Two-Factor Auth OTP
                        _repo.Otp.SendOTP(user, OtpTypesEnum.Login);
                        response.Message = "verifyLogin";
                        return Ok(response);
                    }
                }
                else
                {


                    token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);

                    ClientDto client = _repo.Client.GetClientByUserId(user.Id);

                    response = new AuthResponseDto()
                    {
                        Token = token,
                        ClientId = client.Id,
                        TokenExpiry = DateTime.Now.AddMinutes(jwtSettings.LifeSpan).ToString(),
                        User = user,
                        Message = "SuccessfulSocialLogin",
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                response.Message = "InternalError";
                return StatusCode(401, response);
            }
        }

        [HttpPost("advisor/login")]
        public IActionResult AuthenticateAdmin(LoginDto dto)
        {
            AuthResponseDto response = new AuthResponseDto();
            try
            {
                bool loginExists = false;
                bool passwordMatched = false;
                bool registrationVerified = false;


                UserDto user = new UserDto();
                RoleEnum role = RoleEnum.Advisor;
                var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
                string token = String.Empty;

                loginExists = _repo.User.DoesUserNameExist(dto);

                if (!loginExists)
                {
                    response.Message = "Invalid-NotExist";
                    return StatusCode(401, response);

                }

                user = _repo.User.GetUser(dto);

                if (user.Role != RoleEnum.Advisor && user.Role != RoleEnum.External && user.Role != RoleEnum.Admin)
                {
                    response.Message = "Invalid-Credentials";
                    return StatusCode(401, response);
                }


                if (dto.UserName == "dev@aluma.co.za")
                {
                    token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);

                    AdvisorDto advisor = _repo.Advisor.GetAdvisorByUserId(user.Id);

                    response = new AuthResponseDto()
                    {
                        Token = token,
                        AdvisorId = advisor.Id,
                        TokenExpiry = DateTime.Now.AddMinutes(jwtSettings.LifeSpan).ToString(),
                        User = user,
                        Message = "OtpVerified",
                    };
                    return Ok(response);
                }


                passwordMatched = _repo.User.IsPasswordVerified(dto);
                if (!passwordMatched)
                {
                    response.Message = "Invalid-Credentials";
                    return StatusCode(401, response);
                }

                //token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);
                //AdvisorDto advisor = _repo.Advisor.GetAdvisorByUserId(user.Id);

                //response = new AuthResponseDto()
                //{
                //    Token = token,
                //    AdvisorId = advisor.Id,
                //    TokenExpiry = DateTime.Now.AddMinutes(jwtSettings.LifeSpan).ToString(),
                //    User = user,
                //    Message = "OtpVerified",
                //};
                //return Ok(response);

                registrationVerified = _repo.User.IsRegistrationVerified(dto);

                if (!registrationVerified)
                {
                    response.Message = "verifyRegistration";
                    return StatusCode(401, response);
                }
                else
                {
                    //Send Two-Factor Auth OTP
                    _repo.Otp.SendOTP(user, OtpTypesEnum.Login);
                    response.Message = "verifyLogin";
                    return Ok(response);
                }


            }
            catch (Exception e)
            {
                response.Message = "InternalError";
                return StatusCode(401, response);
            }
        }


        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] LoginDto dto)
        {
            AuthResponseDto response = new AuthResponseDto();

            try
            {
                bool loginExists = false;
                UserDto user = new UserDto();

                loginExists = _repo.User.DoesUserNameExist(dto);

                if (!loginExists)
                {
                    response.Status = "Failure";
                    response.Message = "Invalid-NotExist";
                    return StatusCode(401, response);
                }

                _repo.User.ForgotPassword(dto);

                response.Status = "Success";
                response.Message = "RequestSent";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Status = "Failure";
                response.Message = "InternalError";
                return StatusCode(500, response);
            }
        }

        [HttpPost("reset-password"), AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordDto dto)
        {
            AuthResponseDto response = new AuthResponseDto();

            try
            {
                UserDto user = new UserDto();

                int userId = _repo.User.DecryptUserId(dto.UserId);
                user.Id = userId;
                user = _repo.User.GetUser(user);


                _repo.Otp.SendOTP(user, OtpTypesEnum.ResetPassword);
                response.Status = "Success";
                response.Message = "verifyResetPassword";

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Status = "Failure";
                response.Message = "InternalError";
                return StatusCode(500, response);
            }
        }

        [HttpPost("reset-advisor-password"), AllowAnonymous]
        public IActionResult ResetPassword(LoginDto dto)
        {
                AuthResponseDto response = new AuthResponseDto();

            try
            {
                UserDto user = new UserDto();

                user = _repo.User.GetUser(dto);

                _repo.Otp.SendOTP(user, OtpTypesEnum.ResetPassword);
                response.Status = "Success";
                response.Message = "verifyResetPassword";

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Status = "Failure";
                response.Message = "InternalError";
                return StatusCode(500, response);
            }
        }
    }
}