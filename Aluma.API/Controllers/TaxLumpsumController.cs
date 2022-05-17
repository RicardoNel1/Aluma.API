using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class TaxLumpsumController : ControllerBase
    {
        private readonly IWrapper _repo;

        public TaxLumpsumController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetTaxLumpsum(int clientId)
        {
            return null;
        }

        [HttpPost]
        public IActionResult CreateTaxLumpsum(TaxLumpSumDto dto)
        {
            return null;
        }

        [HttpPut]
        public IActionResult UpdateTaxLumpsum(TaxLumpSumDto dto)
        {
            return null;
        }

        [HttpDelete]
        public IActionResult DeleteTaxLumpsum(int clientId)
        {
            return null;
        }


    }
}
