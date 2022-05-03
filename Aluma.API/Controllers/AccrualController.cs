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
                var exists = _repo.Accrual.Exists(dto.ClientId);

                if (exists)
                {
                    _repo.Accrual.UpdateAccrual(dto);
                }
                else
                {
                    _repo.Accrual.CreateAccrual(dto);
                }
                
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getAccrual"), AllowAnonymous]
        public IActionResult GetAccrual(int ClientId)
        {
            try
            {
                AccrualDto accrual = _repo.Accrual.GetAccrual(ClientId);
                return Ok(accrual);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
