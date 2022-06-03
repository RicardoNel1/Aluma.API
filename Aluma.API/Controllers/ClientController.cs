using Aluma.API.RepoWrapper;
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
    public class ClientController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ClientController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetClient(int clientId)
        {
            ClientDto dto = new();
            try
            {
                dto = _repo.Client.GetClient(new ClientDto() { Id = clientId });

                dto.Status = "Success";

                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpGet("getClientByUserID"), AllowAnonymous]
        public IActionResult GetClientByUserId(int userId)
        {
            try
            {
                ClientDto client = _repo.Client.GetClientByUserId(userId);

                return Ok(client);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateClient(ClientDto dto)
        {
            try
            {
                bool clientExist = _repo.Client.DoesClientExist(dto);
                if (clientExist)
                {
                    return BadRequest("Client Exists");
                }

                dto = await _repo.Client.CreateClient(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("passports"), AllowAnonymous]
        public IActionResult UpdateClientPassports(List<PassportDto> dto)
        {
            try
            {

                _repo.Client.UpdateClientPassports(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public async Task<IActionResult> UpdateClient(ClientDto dto)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                UserDto user = new();
                bool clientExist = _repo.Client.DoesClientExist(dto);
                bool idExists = _repo.Client.DoesIDExist(dto);
                if (idExists)
                {
                    dto.Status = "Failure";
                    dto.Message = "Invalid-RSAID";
                    return StatusCode(405, dto);
                }
                else if (!clientExist)
                {

                    //RegistrationDto registerDto = new RegistrationDto
                    //{
                    //    FirstName = dto.User.FirstName,
                    //    LastName = dto.User.LastName,
                    //    Email = dto.User.Email,
                    //    MobileNumber = dto.User.MobileNumber,
                    //};

                    ////Create User
                    //user = _repo.User.CreateClientUser(registerDto);

                    //Create Client
                    //dto.UserId = user.Id;
                   
                    dto = await _repo.Client.CreateClient(dto);

                    dto.Status = "Success";
                    return Ok(dto);
                }
                else
                {

                    _repo.Client.UpdateClient(dto);
                    dto.Status = "Success";
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteClient(ClientDto dto)
        {
            try
            {
                bool isDeleted = _repo.Client.DeleteClient(dto);
                if (!isDeleted)
                {
                    return BadRequest("Client Not Deleted");
                }
                return Ok("Client Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> RegisterClient(RegistrationDto dto)
        {
            AuthResponseDto response = new();
            UserDto user = null;
            ClientDto client = null;

            try
            {
                bool userExists = _repo.Client.DoesClientExist(dto);

                if (userExists)
                {
                    response.Message = "Invalid-Exists";
                    return StatusCode(403, response);
                }
                else
                {
                    //Create User
                    user = _repo.User.CreateClientUser(dto);

                    //Create Client
                    client = new ClientDto() { UserId = user.Id, AdvisorId = null, ClientType = "Primary" };
                    client = await _repo.Client.CreateClient(client);

                    //Send Verification OTP
                    string sendResult = _repo.Otp.SendOTP(user, OtpTypesEnum.Registration);

                    if (sendResult != "Success")
                    {
                        response.Message = sendResult;
                        return StatusCode(403, response);
                    }

                    response.Message = "verifyRegistration";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                response.Message = "InternalError";
                Console.WriteLine(e.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("list/advisor/{advisorId}"), AllowAnonymous]
        public IActionResult ListAdvisorClients(int advisorId)
        {
            try
            {
                var clientList = _repo.Client.GetClientsByAdvisor(advisorId);

                return Ok(clientList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list/admin"), AllowAnonymous]
        public IActionResult ListAllClients()
        {
            try
            {
                var clientList = _repo.Client.GetClients();

                return Ok(clientList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}