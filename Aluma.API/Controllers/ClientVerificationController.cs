using AutoMapper;
using DataService.Dto.Advisor;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Data;
using System.Threading.Tasks;
using System;
using Aluma.API.RepoWrapper;
using DataService.Dto.ClientVerification;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ClientVerificationController : ControllerBase
    {
        private readonly IWrapper _repo;
        private readonly IMapper _mapper;

        public ClientVerificationController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("submit-facetec"), AllowAnonymous]
        public async Task<IActionResult> SubmitFacetecScreening(FacetecScreeningRequestDto request)
        {
            try
            {
                FacetecScreeningResponsesDto details = _repo.ClientVerificationServiceRepo.SubmitFacetecScreening(request);

                return Ok(details);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("submit"), AllowAnonymous]
        public async Task<IActionResult> SubmitAMLScreening(AMLScreeningSubmitRequestDto request)
        {
            AMLScreeningSubmitResponseDto response = new();
            try
            {
                response = _repo.ClientVerificationServiceRepo.SubmitAMLScreening(request);

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Status = "Error";
                return StatusCode(500, response);
                response.Message = "InternalError";
            }
        }


        [HttpPost("result"), AllowAnonymous]
        public async Task<IActionResult> ResultAMLScreening(AMLScreeningResultRequestDto request)
        {
            AMLScreeningResultResponseDto response = new();
            try
            {
                response = _repo.ClientVerificationServiceRepo.ResultAMLScreening(request);

                return Ok(response);
            }
            catch (Exception e)
            {
                response.Status = "Error";
                response.Message = e.Message;
                return StatusCode(500, response);
            }
        }
    }
}
