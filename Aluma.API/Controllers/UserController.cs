using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class UserController : Controller
    {
        private readonly IWrapper _repo;

        public UserController(IWrapper wrapper)
        {
            _repo = wrapper;
        }

        [HttpPut]
        public IActionResult GetUser(UserDto dto)
        {
            try
            {
                var user = _repo.User.GetUser(dto);

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("signature")]
        public IActionResult GetSignature(UserDto dto)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                string signature = _repo.User.GetUserSignature(dto);

                return Ok(signature);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("edit/signature"), AllowAnonymous]
        public IActionResult EditUserSignature(int userId, byte[] signature)
        {
            try
            {
                bool updated = _repo.User.EditUserSignature(userId, signature);

                return Ok(updated);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //[HttpPost, AllowAnonymous] //testing
        //public IActionResult CreateClientUser(RegistrationDto dto)
        //{
        //    try
        //    {
        //        //bool clientExist = _repo.Client.DoesClientExist(dto);
        //        //if (clientExist)
        //        //{
        //        //    return BadRequest("Client Exists");
        //       // }

        //        dto = _repo.User.CreateClientUser(dto);

        //        return Ok(dto);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
    }
}