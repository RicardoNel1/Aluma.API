﻿using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

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
        public IActionResult GetClient(int userId)
        {
            try
            {
                ClientDto client = _repo.Client.GetClient(userId);

                return Ok(client);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateClient(ClientDto dto)
        {
            try
            {
                bool clientExist = _repo.Client.DoesClientExist(dto);
                if (clientExist)
                {
                    return BadRequest("Client Exists");
                }

                dto = _repo.Client.CreateClient(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateClient(ClientDto dto)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                bool clientExist = _repo.Client.DoesClientExist(dto);
                if (!clientExist)
                {
                    return BadRequest("Client Does Not Exist");
                }
                else { 

                _repo.Client.UpdateClient(dto);
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
        public IActionResult RegisterClient(RegistrationDto dto) 
        {
            AuthResponseDto response = new AuthResponseDto();
            UserDto user = null;
            ClientDto client = null;

            try
            {
                bool userExists = _repo.Client.DoesClientExist(dto);
              
                if (userExists)
                {
                    response.Message = "Client already exists. Please sign in.";
                    return StatusCode(403, response);
                }
                else
                {
                    //Todo: CodeReview discussion.

                    //Create User
                    user = _repo.User.CreateClientUser(dto);

                    //Create Client
                    client = new ClientDto() { UserId = user.Id, AdvisorId = null, ClientType = "Primary" };
                    client = _repo.Client.CreateClient(client);

                    //Send Verification OTP
                    string sendResult = _repo.Otp.SendOTP(user,OtpTypesEnum.Registration);

                    if (sendResult != "Success" )
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
                response.Message = e.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("list/advisor"), Authorize(Roles = "Advisor,Admin")]
        public IActionResult ListAdvisorClients(AdvisorDto dto)
        {
            try
            {
                var clientList = _repo.Client.GetClientsByAdvisor(dto); ;

                return Ok(clientList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list/admin"), Authorize(Roles = "Admin")]
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