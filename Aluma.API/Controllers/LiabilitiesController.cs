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
        [HttpPut("liabilities"), AllowAnonymous]
        public IActionResult UpdateLiabilities([FromBody] LiabilitiesDto[] dtoArray)
        {
            try
            {
                _repo.Liabilities.UpdateLiabilities(dtoArray);
                return Ok("Liabilities Updated");
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
        [HttpDelete("liabilities"), AllowAnonymous]
        public IActionResult DeleteLiabilitiesItem(int id)
        {
            try
            {
                bool deleted = _repo.Liabilities.DeleteLiabilitiesItem(id);

                return Ok(deleted);
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
    }
}