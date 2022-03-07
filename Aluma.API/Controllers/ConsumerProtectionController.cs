using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ConsumerProtectionController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ConsumerProtectionController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateConsumerProtection([FromBody] ConsumerProtectionDto dto)
        {
            try
            {
                bool consumerProtectionExists = _repo.ConsumerProtection.DoesConsumerProtectionExist(dto);                

                if (consumerProtectionExists)
                {
                    return BadRequest("Consumer Protection Exists");
                }
                else 
                { 
                     _repo.ConsumerProtection.CreateConsumerProtection(dto);
                }
                return Ok("Consumer Protection Created");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateConsumerProtection([FromBody] ConsumerProtectionDto dto)
        {
            try
            {
                bool consumerProtectionExists = _repo.ConsumerProtection.DoesConsumerProtectionExist(dto);               

                if (!consumerProtectionExists)
                {
                    CreateConsumerProtection(dto);
                }
                else 
                { 
                    _repo.ConsumerProtection.UpdateConsumerProtection(dto);
                }

                return Ok("Consumer Protection Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetConsumerProtection(int clientId)
        {
            try
            {
                ConsumerProtectionDto consumerProtection = _repo.ConsumerProtection.GetConsumerProtection(clientId);

                return Ok(consumerProtection);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
               

    }
}