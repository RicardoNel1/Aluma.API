using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateClientFNA(int clientId)
        {
            ClientFNADto dto = new ClientFNADto();

            try
            {
                var fnaExist = _repo.Client.CheckForFNA(new ClientDto() { Id = clientId});
                if (fnaExist.hasFNA)
                {
                    dto.Status = "Failure";
                    dto.Message = "FNA Exists";
                    return BadRequest(dto);
                }

                dto = await _repo.FNA.CreateFNA(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = "";
                return StatusCode(500, e.Message);
            }
        }


    }
}