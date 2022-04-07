using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class FNAController : ControllerBase
    {
        private readonly IWrapper _repo;

        public FNAController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost("assets"), AllowAnonymous]
        public IActionResult CreateAssets([FromBody] AssetsDto dto)
        {
            try
            {
                //bool assetExists = _repo.ConsumerProtection.DoesConsumerProtectionExist(dto);                

                //if (consumerProtectionExists)
                //{
                //    return BadRequest("Consumer Protection Exists");
                //}
                //else 
                //{ 


                _repo.Assets.CreateAssets(dto);
                return Ok(dto);
                //}
                //return Ok("Consumer Protection Created");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("assets"), AllowAnonymous]
        public IActionResult UpdateAssets([FromBody] AssetsDto dto)
        {
            try
            {
                //bool consumerProtectionExists = _repo.ConsumerProtection.DoesConsumerProtectionExist(dto);               

                //if (!consumerProtectionExists)
                //{
                //    CreateConsumerProtection(dto);
                //}
                //else 
                //{ 
                    _repo.Assets.UpdateAssets(dto);
                //}

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[HttpGet, AllowAnonymous]
        //public IActionResult GetAsset(int clientId)
        //{
        //    try
        //    {
        //        ConsumerProtectionDto consumerProtection = _repo.ConsumerProtection.GetConsumerProtection(clientId);

        //        return Ok(consumerProtection);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
               

    }
}