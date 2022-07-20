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
    public class ClientPortfolioController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ClientPortfolioController(IWrapper repo)
        {
            _repo = repo;
        }
                

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetClientPortfolio(int clientId)
        {
            ClientPortfolioDto dto = new();
            try
            {
                dto = await _repo.ClientPortfolio.GetClientPortfolio(clientId);

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

        [HttpPut("notes"), AllowAnonymous]
        public IActionResult UpdateClientNotes([FromBody] List<ClientNotesDto> dtoArray)
        {
            try
            {
                var updated = _repo.ClientPortfolio.CreateClientNote(dtoArray);
                return Ok(updated);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("notes/delete"), AllowAnonymous]
        public IActionResult DeleteClientNotesItem(int Id)
        {
            try
            {
                string result = _repo.ClientPortfolio.DeleteClientNote(Id);

                if (result.ToLower().Contains("success"))
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}