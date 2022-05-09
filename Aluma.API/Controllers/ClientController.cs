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
        #region Private Fields

        private readonly IWrapper _repo;

        #endregion Private Fields

        #region Public Constructors

        public ClientController(IWrapper repo)
        {
            _repo = repo;
        }

        #endregion Public Constructors

        #region Public Methods

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

        [HttpGet, AllowAnonymous]
        public IActionResult GetClient(int clientId)
        {
            try
            {
                ClientDto client = _repo.Client.GetClient(new ClientDto() { Id = clientId });

                return Ok(client);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
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

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> RegisterClient(RegistrationDto dto)
        {
            AuthResponseDto response = new AuthResponseDto();
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

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateClient(ClientDto dto)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                bool clientExist = _repo.Client.DoesClientExist(dto);
                bool idExists = _repo.Client.DoesIDExist(dto);
                if (!clientExist)
                {
                    return BadRequest("Client Does Not Exist");
                }
                else if (idExists)
                {
                    dto.Status = "Failure";
                    dto.Message = "Invalid-RSAID";
                    return StatusCode(405, dto);
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

        #endregion Public Methods
    }
}