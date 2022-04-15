using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class LiabilitiesController : ControllerBase
    {
        private readonly IWrapper _repo;

        public LiabilitiesController(IWrapper repo)
        {
            _repo = repo;
        }
             

        //Liabilities      
        [HttpPut("liabilities"), AllowAnonymous]
        public IActionResult UpdateLiabilities([FromBody] LiabilitiesDto[] dtoArray)
        {
            try
            {
                _repo.Liabilities.UpdateLiabilities(dtoArray);
                return Ok("Liabilities Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("liabilities"), AllowAnonymous]
        public IActionResult GetLiabilities(int clientId)
        {
            try
            {
                List<LiabilitiesDto> dtoList = _repo.Liabilities.GetLiabilities(clientId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //Estate Expenses      
        //[HttpPut("liabilities"), AllowAnonymous]
        //public IActionResult UpdateLiabilities([FromBody] LiabilitiesDto[] dtoArray)
        //{
        //    try
        //    {
        //        _repo.Liabilities.UpdateLiabilities(dtoArray);
        //        return Ok("Liabilities Updated");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("liabilities"), AllowAnonymous]
        //public IActionResult GetLiabilities(int clientId)
        //{
        //    try
        //    {
        //        List<LiabilitiesDto> dtoList = _repo.Liabilities.GetLiabilities(clientId);

        //        return Ok(dtoList);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
    }
}