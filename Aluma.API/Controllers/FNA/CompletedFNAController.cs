using Aluma.API.Helpers;
using Aluma.API.Repositories;
using Aluma.API.Repositories.FNA;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class CompletedFNAController : ControllerBase
    {
        private readonly IWrapper _repo;
        MailSender _ms;
        public CompletedFNAController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet("fnaweekly"), AllowAnonymous]
        [AutomaticRetry(Attempts = 30, DelaysInSeconds = new int[] { 60 })]
        public async Task<IActionResult> FNAWeeklyReportAsync()
        {
            try
            {
                var dtoList = await _repo.CompletedFNA.GetCompletedFNA();
                               
                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //public IActionResult ScheduleFNAWeeklyReport()
        //{
        //    RecurringJob.AddOrUpdate<CompletedFNAController>(x => x.FNAWeeklyReportAsync(), Cron.Daily(13, 51));
        //    return Ok();
        //}
    }
}
