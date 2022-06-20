using System.Collections.Generic;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ShortTermInsuranceController : Controller
    {
        private readonly IWrapper _repo;

        public ShortTermInsuranceController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetShortTerm(int clientId)
        {
            return Ok();
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateShortTerm([FromBody] List<SortTermInsuranceDTO> dtoArray)
        {
            return Ok();
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateShortTerm([FromBody] List<SortTermInsuranceDTO> dtoArray)
        {
            return Ok();
        }

        [HttpDelete, AllowAnonymous]
        public IActionResult DeleteShortTerm(int id)
        {
            return NoContent();
        }
    }
}