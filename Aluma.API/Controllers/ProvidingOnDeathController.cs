using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ProvidingOnDeathController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ProvidingOnDeathController(IWrapper repo)
        {
            _repo = repo;
        }


        [HttpPost, AllowAnonymous]
        public IActionResult CreateProvidingOnDeath([FromBody] ProvidingOnDeathDto dto)
        {
            try
            {
                bool providingOnDeathExist = _repo.ProvidingOnDeath.DoesProvidingOnDeathExist(dto);

                if (providingOnDeathExist)
                {
                    return BadRequest("Providing On Death Exists");
                }
                else
                {
                    _repo.ProvidingOnDeath.CreateProvidingOnDeath(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateProvidingOnDeath([FromBody] ProvidingOnDeathDto dto)
        {
            try
            {
                bool providingOnDeathExist = _repo.ProvidingOnDeath.DoesProvidingOnDeathExist(dto);

                if (!providingOnDeathExist)
                {
                    CreateProvidingOnDeath(dto);
                }
                else
                {
                    _repo.ProvidingOnDeath.UpdateProvidingOnDeath(dto);
                }

                return Ok("Providing On Death Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetProvidingOnDeath(int clientId)
        {
            try
            {
                ProvidingOnDeathDto dto = _repo.ProvidingOnDeath.GetProvidingOnDeath(clientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}