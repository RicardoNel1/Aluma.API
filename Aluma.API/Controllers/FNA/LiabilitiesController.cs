using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class LiabilitiesController : ControllerBase
    {
        private readonly IWrapper _repo;

        public LiabilitiesController(IWrapper repo)
        {
            _repo = repo;
        }
             

        //Liabilities      
        [HttpPut("liabilities/update"), AllowAnonymous]
        public IActionResult UpdateLiabilities([FromBody] LiabilitiesDto[] dtoArray)
        {
            try
            {
                _repo.Liabilities.UpdateLiabilities(dtoArray);
                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("liabilities"), AllowAnonymous]
        public IActionResult GetLiabilities(int fnaId)
        {
            try
            {
                List<LiabilitiesDto> dtoList = _repo.Liabilities.GetLiabilities(fnaId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete("liabilities/delete"), AllowAnonymous]
        public IActionResult DeleteLiabilitiesItem([FromQuery] int Id)
        {

            try
            {
                string result = _repo.Liabilities.DeleteLiabilities(Id);

                if (result.ToLower().Contains("success"))
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }


        //Estate Expenses    
        [HttpPost("estate_expenses"), AllowAnonymous]
        public IActionResult CreateEstateExpenses([FromBody] EstateExpensesDto dto)
        {
            try
            {
                bool estateExpensesExists = _repo.EstateExpenses.DoesEstateExpensesExist(dto);

                if (estateExpensesExists)
                {
                    return BadRequest("Estate Expenses Exists");
                }
                else
                {
                    _repo.EstateExpenses.CreateEstateExpenses(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("estate_expenses"), AllowAnonymous]
        public IActionResult UpdateEstateExpenses([FromBody] EstateExpensesDto dto)
        {
            try
            {
                bool estateExpensesExist = _repo.EstateExpenses.DoesEstateExpensesExist(dto);

                if (!estateExpensesExist)
                {
                    CreateEstateExpenses(dto);
                }
                else
                {
                    _repo.EstateExpenses.UpdateEstateExpenses(dto);
                }

                return Ok("Estate Expenses Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("estate_expenses"), AllowAnonymous]
        public IActionResult GetEstateExpenses(int fnaId)
        {
            try
            {
                EstateExpensesDto dto = _repo.EstateExpenses.GetEstateExpenses(fnaId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //Administration Costs    
        [HttpPost("administration_costs"), AllowAnonymous]
        public IActionResult CreateAdministrationCosts([FromBody] AdministrationCostsDto dto)
        {
            try
            {
                bool administrationCostsExist = _repo.AdministrationCosts.DoesAdministrationCostsExist(dto);

                if (administrationCostsExist)
                {
                    return BadRequest("Estate Expenses Exists");
                }
                else
                {
                    _repo.AdministrationCosts.CreateAdministrationCosts(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("administration_costs"), AllowAnonymous]
        public IActionResult UpdateAdministrationCosts([FromBody] AdministrationCostsDto dto)
        {
            try
            {
                bool administrationCostsExists = _repo.AdministrationCosts.DoesAdministrationCostsExist(dto);

                if (!administrationCostsExists)
                {
                    CreateAdministrationCosts(dto);
                }
                else
                {
                    _repo.AdministrationCosts.UpdateAdministrationCosts(dto);
                }

                return Ok("Administration Costs Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("administration_costs"), AllowAnonymous]
        public IActionResult GetAdministrationCosts(int fnaId)
        {
            try
            {
                AdministrationCostsDto dto = _repo.AdministrationCosts.GetAdministrationCosts(fnaId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //Estate Duties
        [HttpPost("estate_duties"), AllowAnonymous]
        public IActionResult CreateEstateDuties([FromBody] EstateDutyDto dto)
        {
            try
            {
                bool estateDutyExists = _repo.EstateDuties.DoesEstateDutyExist(dto);

                if (estateDutyExists)
                {
                    return BadRequest("Estate Duty Exists");
                }
                else
                {
                    _repo.EstateDuties.CreateEstateDuty(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("estate_duties"), AllowAnonymous]
        public IActionResult UpdateEstateDuty([FromBody] EstateDutyDto dto)
        {
            try
            {
                bool estateDutyExists = _repo.EstateDuties.DoesEstateDutyExist(dto);

                if (!estateDutyExists)
                {
                    CreateEstateDuties(dto);
                }
                else
                {
                    _repo.EstateDuties.UpdateEstateDuty(dto);
                }

                return Ok("Estate Duties Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("estate_duties"), AllowAnonymous]
        public IActionResult GetEstateDuty(int fnaId)
        {
            try
            {
                EstateDutyDto dto = _repo.EstateDuties.GetEstateDuty(fnaId);

                return Ok(dto);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        //Capital Gains Tax
        [HttpPost("capital_gains_tax"), AllowAnonymous]
        public IActionResult CreateCapitalGainsTax([FromBody] CapitalGainsTaxDto dto)
        {
            try
            {
                bool estateDutyExists = _repo.CapitalGainsTax.DoesCapitalGainsTaxExist(dto);

                if (estateDutyExists)
                {
                    return BadRequest("Capital Gains Tax Exists");
                }
                else
                {
                    _repo.CapitalGainsTax.CreateCapitalGainsTax(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("capital_gains_tax"), AllowAnonymous]
        public IActionResult UpdateCapitalGainsTax([FromBody] CapitalGainsTaxDto dto)
        {
            try
            {
                bool estateDutyExists = _repo.CapitalGainsTax.DoesCapitalGainsTaxExist(dto);

                if (!estateDutyExists)
                {
                    CreateCapitalGainsTax(dto);
                }
                else
                {
                    _repo.CapitalGainsTax.UpdateCapitalGainsTax(dto);
                }

                return Ok("Capital Gains Tax Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("capital_gains_tax"), AllowAnonymous]
        public IActionResult GetCapitalGainsTax(int fnaId)
        {
            try
            {
                CapitalGainsTaxDto dto = _repo.CapitalGainsTax.GetCapitalGainsTax(fnaId);

                return Ok(dto);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
    }
}