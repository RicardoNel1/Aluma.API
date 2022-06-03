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

                dto.Status = "Success";
                dto.Message = "Providing On Dread Disease Created";

                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
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

                dto.Status = "Success";
                dto.Message = "Providing On Dread Disease Created";

                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetProvidingOnDreadDisease(int fnaId)
        {
            try
            {
                ProvidingOnDreadDiseaseDto dto = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(fnaId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}
