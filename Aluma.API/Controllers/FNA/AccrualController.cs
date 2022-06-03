using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    //Send the work to be done to the Repo
    [ApiController, Route("api/[controller]"), Authorize]
    public class AccrualController : ControllerBase
    {
        private readonly IWrapper _repo;

        public AccrualController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost("accrual"), AllowAnonymous]
        public IActionResult CreateClientAccrual([FromBody] AccrualDto dto)
        {
            try
            {
                var exists = _repo.Accrual.Exists(dto.FNAId);

                if (exists)
                {
                    dto.Status = "Success";
                    dto.Message = "Accrual Record Updated";
                    _repo.Accrual.UpdateAccrual(dto);
                }
                else
                {
                    dto.Status = "Success";
                    dto.Message = "Accrual Record Created";
                    _repo.Accrual.CreateAccrual(dto);
                }
                
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failed";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpGet("getAccrual"), AllowAnonymous]
        public IActionResult GetAccrual(int fnaId)
        {
            try
            {
                AccrualDto accrual = _repo.Accrual.GetAccrual(fnaId);
                return Ok(accrual);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
