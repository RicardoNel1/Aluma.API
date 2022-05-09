using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly IWrapper _repo;

        #endregion Private Fields

        #region Public Constructors

        public ProductController(IWrapper repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost, AllowAnonymous]
        public IActionResult CreateProduct(ProductDto dto)
        {
            try
            {
                bool productExists = _repo.ProductRepo.DoesProductExist(dto);
                if (productExists)
                {
                    return BadRequest("Product Exists");
                }

                dto = _repo.ProductRepo.CreateProduct(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("productId"), AllowAnonymous]
        public IActionResult GetProduct(int productId)
        {
            ProductDto dto = null;
            try
            {
                //ClientDto client = _repo.Client.GetClient(userId);
                dto = _repo.ProductRepo.GetProduct(productId);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, dto);
            }
        }

        [HttpGet("list"), AllowAnonymous]
        public IActionResult GetProductList()
        {
            try
            {
                //ClientDto client = _repo.Client.GetClient(userId);
                List<ProductDto> products = _repo.ProductRepo.GetAllProducts();

                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, null);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateProduct([FromBody] ProductDto dto)
        {
            try
            {
                bool productExists = _repo.ProductRepo.DoesProductExist(dto);
                if (!productExists)
                {
                    return BadRequest("Product Does Not Exist");
                }

                dto = _repo.ProductRepo.UpdateProduct(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion Public Methods
    }
}