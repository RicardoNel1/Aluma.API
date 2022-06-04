using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class AssetSummaryController : ControllerBase
    {
        private readonly IWrapper _repo;

        public AssetSummaryController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetAssetSummary(int fnaId)
        {
            AssetSummaryDto dto = new();
            try
            {
                dto = _repo.AssetSummary.GetAssetSummary(fnaId);

                dto.Status = "Success";
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateAssetSummary([FromBody] AssetSummaryDto dto)
        {
            try
            {
                dto = _repo.AssetSummary.UpdateAssetSummary(dto);
                dto.Status = "Success";

                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Failure";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }


    }
}