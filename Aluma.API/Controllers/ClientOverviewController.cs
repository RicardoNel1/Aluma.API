using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ClientOverviewController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ClientOverviewController(IWrapper repo)
        {
            _repo = repo;
        }
                

        [HttpGet, AllowAnonymous]
        public IActionResult GetClientOverview(int fnaId)
        {
            ClientOverviewDto dto = new();
            try
            {
                dto = _repo.ClientOverview.GetClientOverview(fnaId);

                dto.Status = "Success";
                dto.Message = "";
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }







    }
}