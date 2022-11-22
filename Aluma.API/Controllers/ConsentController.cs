using Aluma.API.Repositories;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Dto.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsentController : ControllerBase
    {
        private readonly IWrapper _repo;

        public ConsentController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult SaveConsentForm([FromBody] List<ClientConsentDto> dtoArray)
        {
            try
            {
                _repo.Client.SaveConsentForm(dtoArray);
                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("financial-providers")]
        public IActionResult GetFinancialProviders()
        {
            List<FinancialProviderDto> dto = new();
            try
            {
                dto = _repo.Client.GetFinancialProviders();

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, dto);
            }
        }

        [HttpGet("client-consented-providers")]
        public IActionResult GetClientConsentedProviders(int ClientId)
        {
            List<ClientConsentDto> dto = new();
            try
            {
                dto = _repo.Client.GetClientConsentedProviders(ClientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, dto);
            }
        }
    }
}
