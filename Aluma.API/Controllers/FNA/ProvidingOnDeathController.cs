using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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

                dto.Status = "Success";
                dto.Message = "Providing On Death Created";

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

                dto.Status = "Success";
                dto.Message = "Providing On Death Created";

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
        public IActionResult GetProvidingOnDeath(int fnaId)
        {
            ProvidingOnDeathDto dto = new();
            try
            {
                dto = _repo.ProvidingOnDeath.GetProvidingOnDeath(fnaId);

                if (dto == null)
                {
                    dto.Status = "Failure";
                    dto.Message = "NotExist";
                }
                else
                {
                    dto.Status = "Success";
                    dto.Message = "RecordFound";

                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }


    }
}