using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IWrapper _repo;

        public AddressController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetAddress(int userId, string type)
        {
            try
            {
                AddressDto address = _repo.User.GetUserAddress(userId, type);

                return Ok(address);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateAddress(AddressDto dto)
        {
            try
            {
                bool addressExist = _repo.User.DoesAddressExist(dto);

                if (addressExist)
                {
                    return BadRequest("Address Exists");
                }
                else
                {
                    _repo.User.CreateUserAddress(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateAddress(AddressDto dto)
        {
            bool addressExist = _repo.User.DoesAddressExist(dto);


            if (!addressExist)
            {                
                CreateAddress(dto);
            }
            else
            {
                _repo.User.UpdateUserAddress(dto);
            }
            return Ok(dto);
        }
    }
}