using Aluma.API.RepoWrapper;
using DataService.Dto;
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
                    _repo.BankDetails.CreateClientBankDetails(dto);
                }
                return Ok("Bank Details Created");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateClientBankDetails([FromBody] BankDetailsDto dto)
        {
            bool bankDetailsExist = _repo.BankDetails.DoesBankDetailsExist(dto); 

            if (!bankDetailsExist)
            {
                //return BadRequest("Bank Details Doesn't Exist");
                CreateClientBankDetails(dto);
            }
            else
            {
                _repo.BankDetails.UpdateClientBankDetails(dto);
            }
            return Ok("Bank Details Updated");
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