using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Advisor,Admin")]
    public class FSASController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IMapper _mapper;

        public FSASController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCCP(ClientDto dto)
        {

           var advisorUserId =  _repo.JwtRepo.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

            try
            {
                var ccp = _repo.FSASRepo.SubmitClientCCPRequest(dto);
                return Ok(ccp);
                
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        public IActionResult GetFSASInformation(int clientId)
        {
            var advisorUserId = _repo.JwtRepo.GetUserClaims(Request.Headers[HeaderNames.Authorization].ToString());

            try
            {
                var ccp = _repo.FSASRepo.GetClientCCP(clientId);

                return Ok(ccp);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}