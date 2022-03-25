using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            try
            {
                bool loginExists = false;
                bool socialLoginVerified = false;
                bool passwordMatched = false;
                bool registrationVerified = false;

                AuthResponseDto response = new AuthResponseDto();
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

                    return StatusCode(401, "Invalid");
                }

                socialLoginVerified = true;//_repo.User.IsSocialLoginVerified(dto); 

                if (!socialLoginVerified)
                {
                    passwordMatched = _repo.User.IsPasswordVerified(dto);
                    if (!passwordMatched)
                    {
                        response.Message = "Invalid";
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
                    user = _repo.User.GetUser(dto);

                    token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);

                    ClientDto client = _repo.Client.GetClient(user.Id);

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
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("advisor/login")]
        public IActionResult AuthenticateAdmin(LoginDto dto)
        {
            try
            {
                bool loginExists = false;
                bool passwordMatched = false;
                bool registrationVerified = false;

                AuthResponseDto response = new AuthResponseDto();
                UserDto user = new UserDto();
                RoleEnum role = RoleEnum.Advisor;
                var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
                string token = String.Empty;

                loginExists = _repo.User.DoesUserNameExist(dto);

                if (!loginExists)
                {
                    return StatusCode(401, "Invalid");
                }

                passwordMatched = _repo.User.IsPasswordVerified(dto);
                if (!passwordMatched)
                {
                    response.Message = "Invalid";
                    return StatusCode(401, response);
                }

                registrationVerified = _repo.User.IsRegistrationVerified(dto);

                user = _repo.User.GetUser(dto);
                token = _repo.JwtRepo.CreateJwtToken(user.Id, role, jwtSettings.LifeSpan);
                AdvisorDto advisor = _repo.Advisor.GetAdvisor(user.Id);

                response = new AuthResponseDto()
                {
                    Token = token,
                    AdvisorId = advisor.Id,
                    TokenExpiry = DateTime.Now.AddMinutes(jwtSettings.LifeSpan).ToString(),
                    User = user,
                    Message = "OtpVerified",
                };
                return Ok(response);

                //if (!registrationVerified)
                //{
                //    //Re-send Verification OTP
                //    _repo.Otp.SendOTP(user, OtpTypesEnum.Registration);
                //    response.Message = "verifyRegistration";
                //    return StatusCode(401, response);
                //}
                //else
                //{
                //    //Send Two-Factor Auth OTP
                //    _repo.Otp.SendOTP(user, OtpTypesEnum.Login);
                //    response.Message = "verifyLogin";
                //    return Ok(response);
                //}

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[HttpPost("client/register"), AllowAnonymous] //move to clientcontroller
        //public IActionResult RegisterClient(RegistrationDto dto) //create client
        //{
        //    try
        //    {
        //        //bool userExists = _repo.Client.DoesClientExist(dto.);

        //        //bool validId = false;

        //        /*validId = _repo.User.ValidateID(dto.IdNumber);        //register page doesn't ask for ID

        //        check if valid id number has been entered
        //        if (validId == false)
        //        {
        //            return StatusCode(403, "Invalid ID");
        //        }*/



        //        //userExists = _repo.User.DoesUserExist(dto);

        //        //if (userExists)
        //        //{
        //        //    return StatusCode(403, "Duplicate");
        //        //}

        //        _repo.User.CreateClientUser(dto);

        //        //Send Verification OTP
        //        //_repo.Otp.SendRegisterOTP();
        //        return Ok("TwoFactor");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}





        [HttpPost("forgot/password")]
        public IActionResult ForgotPassword([FromBody] LoginDto dto)
        {
            try
            {
                bool loginExists = false;
                bool registrationVerified = false;
                AuthResponseDto response = new AuthResponseDto();
                UserDto user = new UserDto();

                loginExists = _repo.User.DoesUserNameExist(dto);

                if (!loginExists)
                {
                    return StatusCode(401, "Invalid");
                }

                registrationVerified = _repo.User.IsRegistrationVerified(dto);

                if (!registrationVerified)
                {
                    //Re-send Verification OTP
                    _repo.Otp.SendOTP(user, OtpTypesEnum.ResetPassword);
                    return StatusCode(401, "Register Verify");
                }

                _repo.User.ForgotPassword(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("reset/password"), Authorize]
        public IActionResult ResetPassword([FromBody] LoginDto dto)
        {
            try
            {
                bool passwordMatched = false;

                passwordMatched = _repo.User.IsPasswordVerified(dto);

                if (!passwordMatched)
                {
                    return StatusCode(401, "Invalid");
                }

                _repo.User.ResetPassword(dto);
                return Ok("Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}