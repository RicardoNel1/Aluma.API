using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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



        //Assets Attracting CGT
        [HttpPost("assets_attracting_cgt"), AllowAnonymous]
        public IActionResult CreateAssetsAttractingCGT([FromBody] AssetsAttractingCGTDto dto)
        {
            try
            {
                bool assetsAttractingCGTExists = _repo.AssetsAttractingCGT.DoesAssetsAttractingCGTExist(dto);

                if (assetsAttractingCGTExists)
                {
                    return BadRequest("Assets AttractingCGT Exists");
                }
                else
                {
                    _repo.AssetsAttractingCGT.CreateAssetsAttractingCGT(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("assets_attracting_cgt"), AllowAnonymous]
        public IActionResult UpdateAssetsAttractingCGT([FromBody] AssetsAttractingCGTDto dto)
        {
            try
            {
                bool assetsAttractingCGTExist = _repo.AssetsAttractingCGT.DoesAssetsAttractingCGTExist(dto);

                if (!assetsAttractingCGTExist)
                {
                    CreateAssetsAttractingCGT(dto);
                }
                else
                {
                    _repo.AssetsAttractingCGT.UpdateAssetsAttractingCGT(dto);
                }

                return Ok("Assets AttractingCGT Updated");
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
                AssetsAttractingCGTDto dto = _repo.AssetsAttractingCGT.GetAssetsAttractingCGT(clientId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //Assets Exempt from CGT
        [HttpPost("assets_exempt_from_cgt"), AllowAnonymous]
        public IActionResult CreateAssetsExemptFromCGT([FromBody] AssetsExemptFromCGTDto dto)
        {
            try
            {
                bool assetsExemptFromCGTExists = _repo.AssetsExemptFromCGT.DoesAssetsExemptFromCGTExist(dto);

                if (assetsExemptFromCGTExists)
                {
                    return BadRequest("Assets Exempt From CGT Exists");
                }
                else
                {
                    _repo.AssetsExemptFromCGT.CreateAssetsExemptFromCGT(dto);
                }
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("assets_exempt_from_cgt"), AllowAnonymous]
        public IActionResult UpdateAssetsExemptFromCGT([FromBody] AssetsExemptFromCGTDto dto)
        {
            try
            {
                bool assetsExemptFromCGTExist = _repo.AssetsExemptFromCGT.DoesAssetsExemptFromCGTExist(dto);

                if (!assetsExemptFromCGTExist)
                {
                    CreateAssetsExemptFromCGT(dto);
                }
                else
                {
                    _repo.AssetsExemptFromCGT.UpdateAssetsExemptFromCGT(dto);
                }

                return Ok("Primary Residence Updated");
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
                AssetsExemptFromCGTDto dto = _repo.AssetsExemptFromCGT.GetAssetsExemptFromCGT(clientId);

                return Ok(dto);
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