using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IProductRepo : IRepoBase<ProductModel>
    {
        List<ProductDto> GetAllProducts();
        ProductDto GetProduct(int productId);

        bool DoesProductExist(ProductDto dto);

        ProductDto CreateProduct(ProductDto dto);

        ProductDto UpdateProduct(ProductDto dto);

        bool DeleteProduct(ProductDto dto);
    }

    public class ProductRepo : RepoBase<ProductModel>, IProductRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProductRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public ProductDto CreateProduct(ProductDto dto)
        {
            ProductModel newProduct = _mapper.Map<ProductModel>(dto);

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            dto = _mapper.Map<ProductDto>(newProduct);

            return dto;

        }

        public bool DeleteProduct(ProductDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesProductExist(ProductDto dto)
        {
            var rm = _context.Products.Where(r => r.Id == dto.Id);
            if (rm.Any())
            {
                return true;
            }
            return false;

        }

        public ProductDto GetProduct(int productId)
        {
            var productModel = _context.Products.Where(r => r.Id == productId);

            if (productModel.Any())
            {
                return _mapper.Map<ProductDto>(productModel.First());
            }
            return null;

        }

        public List<ProductDto> GetAllProducts()
        {
            List<ProductModel> products = _context.Products.Where(r => r.IsActive == true).ToList();

            if (products.Any())
            {
                return _mapper.Map<List<ProductDto>>(products);
            }
            return null;

        }

        public ProductDto UpdateProduct(ProductDto dto)
        {
            ProductModel newProduct = _mapper.Map<ProductModel>(dto);

            _context.Products.Update(newProduct);
            _context.SaveChanges();

            dto = _mapper.Map<ProductDto>(newProduct);

            return dto;
        }
    }
}