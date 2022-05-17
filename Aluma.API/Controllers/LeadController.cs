using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Admin,Advisor")]
    public class LeadController : ControllerBase
    {
        private readonly IWrapper _repo;

        public LeadController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetLead(int leadId)
        {
            try
            {
                LeadDto lead = _repo.Leads.GetClientLead(leadId);
                return Ok(lead);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateLead(LeadDto dto)
        {
            try
            {
                LeadDto lead = _repo.Leads.CreateClientLead(dto);
                return Ok(lead);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}