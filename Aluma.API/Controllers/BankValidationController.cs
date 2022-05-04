using Aluma.API.RepoWrapper;
using DataService.Dto;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class BankValidationController : ControllerBase
    {
        private readonly IWrapper _repo;

        public BankValidationController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult CreateClientBankDetails([FromBody] BankDetailsDto dto)
        {
            try
            {
                bool bankDetailsExist = _repo.BankDetails.DoesBankDetailsExist(dto);  

                if (bankDetailsExist)
                {
                    return BadRequest("Bank Details Exist");
                }
                else
                {
                   dto = _repo.BankDetails.CreateClientBankDetails(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        [AutomaticRetry(Attempts = 30, DelaysInSeconds = new int[] { 60 })]
        public IActionResult UpdateClientBankDetails([FromBody] BankDetailsDto dto)
        {
            bool bankDetailsExist = _repo.BankDetails.DoesBankDetailsExist(dto); 

            if (!bankDetailsExist)
            {
                //return BadRequest("Bank Details Doesn't Exist");
                dto = _repo.BankDetails.CreateClientBankDetails(dto);
            }
            else
            {
               dto = _repo.BankDetails.UpdateClientBankDetails(dto);
            }
            return Ok(dto);
        }

        [HttpGet, AllowAnonymous]
        //public IActionResult GetClientBankDetails([FromBody] ClientDto dto)
        public IActionResult GetClientBankDetails(int clientId)
        {
            try
            {
                BankDetailsDto bankDetails = _repo.BankDetails.GetBankDetails(clientId);

                return Ok(bankDetails);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteBankDetails([FromBody] BankDetailsDto dto)
        {
            try
            {
                bool isDeleted = _repo.BankDetails.DeleteBankDetails(dto);
                if (!isDeleted)
                {
                    return BadRequest("BankDetails Not Deleted");
                }
                return Ok("BankDetails Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}