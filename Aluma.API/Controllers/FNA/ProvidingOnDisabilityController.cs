using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ProvidingOnDisabilityController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ProvidingOnDisabilityController(IWrapper repo)
        {
            _repo = repo;
        }


        [HttpPost, AllowAnonymous]
        public IActionResult CreateProvidingOnDisability([FromBody] ProvidingOnDisabilityDto dto)
        {
            try
            {
                bool providingOnDisabilityExist = _repo.ProvidingOnDisability.DoesProvidingOnDisabilityExist(dto);

                if (providingOnDisabilityExist)
                {
                    return BadRequest("Providing On Disability Exists");
                }
                else
                {
                    _repo.ProvidingOnDisability.CreateProvidingOnDisability(dto);
                }

                dto.Status = "Success";
                dto.Message = "Providing On Disability Created";

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
        public IActionResult UpdateProvidingOnDisability([FromBody] ProvidingOnDisabilityDto dto)
        {
            try
            {
                bool providingOnDisabilityExist = _repo.ProvidingOnDisability.DoesProvidingOnDisabilityExist(dto);

                if (!providingOnDisabilityExist)
                {
                    return CreateProvidingOnDisability(dto);
                }
                else
                {
                    _repo.ProvidingOnDisability.UpdateProvidingOnDisability(dto);
                }

                dto.Status = "Success";
                dto.Message = "Providing On Disability Updated";

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
        public IActionResult GetProvidingOnDisability(int fnaId)
        {
            try
            {
                ProvidingOnDisabilityDto dto = _repo.ProvidingOnDisability.GetProvidingOnDisability(fnaId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}
