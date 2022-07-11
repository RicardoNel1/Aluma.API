﻿using Aluma.API.RepoWrapper;
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
                _repo.ClientPortfolio.CreateClientNote(dtoArray);
                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}