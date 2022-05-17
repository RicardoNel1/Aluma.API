using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ProvidingOnDreadDiseaseController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ProvidingOnDreadDiseaseController(IWrapper repo)
        {
            _repo = repo;
        }


        [HttpPost, AllowAnonymous]
        public IActionResult CreateProvidingOnDreadDisease([FromBody] ProvidingOnDreadDiseaseDto dto)
        {
            try
            {
                bool providingOnDreadDiseaseExist = _repo.ProvidingOnDreadDisease.DoesProvidingOnDreadDiseaseExist(dto);

                if (providingOnDreadDiseaseExist)
                {
                    return BadRequest("Providing On Dread Disease Exists");
                }
                else
                {
                    _repo.ProvidingOnDreadDisease.CreateProvidingOnDreadDisease(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateProvidingOnDreadDisease([FromBody] ProvidingOnDreadDiseaseDto dto)
        {
            try
            {
                bool providingOnDreadDiseaseExist = _repo.ProvidingOnDreadDisease.DoesProvidingOnDreadDiseaseExist(dto);

                if (!providingOnDreadDiseaseExist)
                {
                    CreateProvidingOnDreadDisease(dto);
                }
                else
                {
                    _repo.ProvidingOnDreadDisease.UpdateProvidingOnDreadDisease(dto);
                }

                return Ok("Providing On Dread Disease Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetProvidingOnDreadDisease(int clientId)
        {
            try
            {
                ProvidingOnDreadDiseaseDto dto = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(clientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}
