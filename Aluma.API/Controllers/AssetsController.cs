﻿using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Dto.FNA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
                    return BadRequest("Primary Residence Exists");
                }
                else
                { 
                    _repo.PrimaryResidence.CreatePrimaryResidence(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
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
                    CreatePrimaryResidence(dto);
                }
                else
                {
                    _repo.PrimaryResidence.UpdatePrimaryResidence(dto);
                }

                return Ok("Primary Residence Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("primary_residence"), AllowAnonymous]
        public IActionResult GetPrimaryResidence(int clientId)
        {
            try
            {
                PrimaryResidenceDto dto = _repo.PrimaryResidence.GetPrimaryResidence(clientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("update_insurance"), AllowAnonymous]
        public IActionResult UpdateInsurance([FromBody] InsuranceDto[] dtoArray)
        {

            return null;
        }


        //Assets Attracting CGT
        [HttpPost("assets_attracting_cgt"), AllowAnonymous] //might never use
        public IActionResult CreateAssetsAttractingCGT([FromBody] AssetsAttractingCGTDto[] dtoArray) 
        {
            try
            {
                    _repo.AssetsAttractingCGT.CreateAssetsAttractingCGT(dtoArray);
           
                return Ok(dtoArray);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("assets_attracting_cgt"), AllowAnonymous]
        public IActionResult UpdateAssetsAttractingCGT([FromBody] AssetsAttractingCGTDto[] dtoArray)
        {
            try
            {
                _repo.AssetsAttractingCGT.UpdateAssetsAttractingCGT(dtoArray);
                return Ok("Assets Attracting CGT Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("assets_attracting_cgt"), AllowAnonymous]
        public IActionResult GetAssetsAttractingCGT(int clientId)
        {            
            try
            {
                List<AssetsAttractingCGTDto> dtoList = _repo.AssetsAttractingCGT.GetAssetsAttractingCGT(clientId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //Assets Exempt from CGT       

        [HttpPut("assets_exempt_from_cgt"), AllowAnonymous]
        public IActionResult UpdateAssetsExemptFromCGT([FromBody] AssetsExemptFromCGTDto[] dtoArray)
        {
            try
            {
                _repo.AssetsExemptFromCGT.UpdateAssetsExemptFromCGT(dtoArray);
                return Ok("Assets Exempt From CGT Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("assets_exempt_from_cgt"), AllowAnonymous]
        public IActionResult GetAssetsExemptFromCGT(int clientId)
        {
            try
            {
                List<AssetsExemptFromCGTDto> dtoList = _repo.AssetsExemptFromCGT.GetAssetsExemptFromCGT(clientId);

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



        //Liquid Assets
        [HttpPost("liquid_assets"), AllowAnonymous]
        public IActionResult CreateLiquidAssets([FromBody] LiquidAssetsDto dto)
        {
            try
            {
                bool liquidAssetsExists = _repo.LiquidAssets.DoesLiquidAssetsExist(dto);

                if (liquidAssetsExists)
                {
                    return BadRequest("Liquid Assets Exists");
                }
                else
                {
                    _repo.LiquidAssets.CreateLiquidAssets(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("liquid_assets"), AllowAnonymous]
        public IActionResult UpdateLiquidAssets([FromBody] LiquidAssetsDto dto)
        {
            try
            {
                bool liquidAssetsExist = _repo.LiquidAssets.DoesLiquidAssetsExist(dto);

                if (!liquidAssetsExist)
                {
                    CreateLiquidAssets(dto);
                }
                else
                {
                    _repo.LiquidAssets.UpdateLiquidAssets(dto);
                }

                return Ok("Liquid Assets Updated");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("liquid assets"), AllowAnonymous]
        public IActionResult GetLiquidAssets(int clientId)
        {
            try
            {
                LiquidAssetsDto dto = _repo.LiquidAssets.GetLiquidAssets(clientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}