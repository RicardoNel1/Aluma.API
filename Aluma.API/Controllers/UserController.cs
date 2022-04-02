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

        [HttpGet("signature"), AllowAnonymous]
        public IActionResult GetSignature(int userId)
        {
            try
            {
                //var claims = _repo.JwtService.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                // string signature = _repo.User.GetUserSignature(userId);
                UserDto dto = _repo.User.GetUserSignature(userId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[HttpPut("edit/signature"), AllowAnonymous]
        //public IActionResult EditUserSignature([FromBody] int userId, string signature)
        ////public IActionResult EditUserSignature(dto)
        //{
        //    try
        //    {
        //        bool updated = _repo.User.EditUserSignature(userId, signature);

        //        return Ok(updated);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        [HttpPut("edit/signature"), AllowAnonymous]
        //public IActionResult EditUserSignature([FromBody] int userId, string signature)
        public IActionResult EditUserSignature([FromBody] UserDto dto)
        {
            try
            {

                _repo.User.EditUserSignature(dto);

                return Ok("Signature Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}