using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IWrapper _repo;

        public AssetsController(IWrapper repo)
        {
            _repo = repo;
        }

        //Primary Residence
        [HttpPost("primary_residence"), AllowAnonymous]
        public IActionResult CreatePrimaryResidence([FromBody] PrimaryResidenceDto dto)
        {
            try
            {
                bool primaryResidenceExists = _repo.PrimaryResidence.DoesPrimaryResidenceExist(dto);

                if (primaryResidenceExists)
                {
                    dto.Status = "BadRequest";
                    dto.Message = "Primary Residence Exists";
                    return BadRequest(dto);
                }
                else
                {
                    dto = _repo.PrimaryResidence.CreatePrimaryResidence(dto);
                }

                dto.Status = "Success";
                dto.Message = "Primary Residence Created";
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpPut("primary_residence"), AllowAnonymous]
        public IActionResult UpdatePrimaryResidence([FromBody] PrimaryResidenceDto dto)
        {
            try
            {
                bool primaryResidenceExist = _repo.PrimaryResidence.DoesPrimaryResidenceExist(dto);

                if (!primaryResidenceExist)
                {
                    return CreatePrimaryResidence(dto);
                }
                else
                {
                    dto = _repo.PrimaryResidence.UpdatePrimaryResidence(dto);
                }

                dto.Status = "Success";
                dto.Message = "Primary Residence Updated";

                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }

        [HttpGet("primary_residence"), AllowAnonymous]
        public IActionResult GetPrimaryResidence(int fnaId)
        {
            PrimaryResidenceDto dto = new();
            try
            {
                dto = _repo.PrimaryResidence.GetPrimaryResidence(fnaId);

                dto.Status = "Success";
                dto.Message = "";
                return Ok(dto);
            }
            catch (Exception e)
            {
                dto.Status = "Server Error";
                dto.Message = e.Message;
                return StatusCode(500, dto);
            }
        }


        //Assets Attracting CGT        

        [HttpPut("assets_attracting_cgt"), AllowAnonymous]
        public IActionResult UpdateAssetsAttractingCGT([FromBody] List<AssetsAttractingCGTDto> dtoArray)
        {
            try
            {
                dtoArray = _repo.AssetsAttractingCGT.UpdateAssetsAttractingCGT(dtoArray);

                if (dtoArray.Where(x => x.Status != "Success" && !string.IsNullOrEmpty(x.Status)).Any())
                    return BadRequest(dtoArray);

                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("assets_attracting_cgt"), AllowAnonymous]
        public IActionResult GetAssetsAttractingCGT(int fnaId)
        {
            try
            {
                List<AssetsAttractingCGTDto> dtoList = _repo.AssetsAttractingCGT.GetAssetsAttractingCGT(fnaId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("assets_attracting_cgt"), AllowAnonymous]
        public IActionResult DeleteAssetsAttractingCGTItem(int id)
        {
            try
            {
                bool deleted = _repo.AssetsAttractingCGT.DeleteAssetsAttractingCGTItem(id);

                return Ok(deleted);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //Assets Exempt from CGT       
        [HttpPut("assets_exempt_from_cgt"), AllowAnonymous]
        public IActionResult UpdateAssetsExemptFromCGT([FromBody] List<AssetsExemptFromCGTDto> dtoArray)
        {
            try
            {
                dtoArray = _repo.AssetsExemptFromCGT.UpdateAssetsExemptFromCGT(dtoArray);

                if (dtoArray.Where(x => x.Status != "Success" && !string.IsNullOrEmpty(x.Status)).Any())
                    return BadRequest(dtoArray);

                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("assets_exempt_from_cgt"), AllowAnonymous]
        public IActionResult GetAssetsExemptFromCGT(int fnaId)
        {
            try
            {
                List<AssetsExemptFromCGTDto> dtoList = _repo.AssetsExemptFromCGT.GetAssetsExemptFromCGT(fnaId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("assets_exempt_from_cgt"), AllowAnonymous]
        public IActionResult DeleteAssetsExemptFromCGTItem(int id)
        {
            try
            {
                bool deleted = _repo.AssetsExemptFromCGT.DeleteAssetsExemptFromCGTItem(id);

                return Ok(deleted);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //Liquid Assets      
        [HttpPut("liquid_assets"), AllowAnonymous]
        public IActionResult UpdateLiquidAssets([FromBody] List<LiquidAssetsDto> dtoArray)
        {
            try
            {
                dtoArray = _repo.LiquidAssets.UpdateLiquidAssets(dtoArray);
                
                if (dtoArray.Where(x => x.Status != "Success" && !string.IsNullOrEmpty(x.Status)).Any())
                    return BadRequest(dtoArray);

                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("liquid_assets"), AllowAnonymous]
        public IActionResult GetLiquidAssets(int fnaId)
        {
            try
            {
                List<LiquidAssetsDto> dtoList = _repo.LiquidAssets.GetLiquidAssets(fnaId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("liquid_assets"), AllowAnonymous]
        public IActionResult DeleteLiquidAssets(int id)
        {
            try
            {
                bool deleted = _repo.LiquidAssets.DeleteLiquidAssetsItem(id);

                return Ok(deleted);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //Insurance      
        [HttpPut("insurance/update"), AllowAnonymous]
        public IActionResult UpdateInsurance([FromBody] List<InsuranceDto> dtoArray)
        {
            try
            {
                _repo.Insurance.UpdateInsurance(dtoArray);
                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("insurance"), AllowAnonymous]
        public IActionResult GetInsurance(int fnaId)
        {
            try
            {
                List<InsuranceDto> dtoList = _repo.Insurance.GetInsurance(fnaId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("insurance/delete"), AllowAnonymous]
        public IActionResult DeleteInsuranceItem(int Id)
        {
            try
            {
                string result = _repo.Insurance.DeleteInsurance(Id);

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

    }
}