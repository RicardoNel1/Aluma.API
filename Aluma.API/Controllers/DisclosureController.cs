using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class DisclosureController : ControllerBase
    {
        private readonly IWrapper _repo;

        public DisclosureController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult CreateDisclosure([FromBody] DisclosureDto dto)
        {
            try
            {
                bool disclosureExists = _repo.Disclosures.DoesClientHaveDisclosure(dto.Client);
                if (disclosureExists)
                {
                    return BadRequest("Disclosure Exists");
                }
                else
                {
                    var disclosure = _repo.Disclosures.CreateDisclosure(dto);
                    return Ok(disclosure);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateDisclosure(ClientDto dto)
        {
            try
            {
                bool disclosureExists = _repo.Disclosures.DoesClientHaveDisclosure(dto);
                if (!disclosureExists)
                {
                    return BadRequest("Disclosure Does Not Exist");
                }
                else
                {
                    _repo.Disclosures.UpdateDisclosure(dto);
                    return Ok("Disclosure Updated");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetDisclosure(ClientDto dto)
        {
            try
            {
                DisclosureDto disclosure = _repo.Disclosures.GetDisclosureByClient(dto);

                return Ok(disclosure);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteDisclosure(DisclosureDto dto)
        {
            try
            {
                bool isDeleted = _repo.Disclosures.DeleteDisclosure(dto);
                if (!isDeleted)
                {
                    return BadRequest("Disclosure Not Deleted");
                }
                return Ok("Disclosure Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("download")]
        public IActionResult DownloadDisclosure(DisclosureDto dto)
        {
            try
            {
                //get base64 string document
                var disclosure = _repo.Disclosures.GetDisclosureDocument(dto);

                return Ok(disclosure);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("list/advisor"), Authorize(Roles = "Advisor,Admin")]
        public IActionResult ListAdvisorDisclosures(AdvisorDto dto)
        {
            try
            {
                //var claims = _repo.JwtRepo.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

                var disclosureList = _repo.Disclosures.GetDisclosureListByAdvisor(dto);

                return Ok(disclosureList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}