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
        #region Public Methods

        ProductDto CreateProduct(ProductDto dto);

        bool DeleteProduct(ProductDto dto);

        bool DoesProductExist(ProductDto dto);

        List<ProductDto> GetAllProducts();
        ProductDto GetProduct(int productId);
        ProductDto UpdateProduct(ProductDto dto);

        #endregion Public Methods
    }

    public class ProductRepo : RepoBase<ProductModel>, IProductRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public ProductRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

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

        public List<ProductDto> GetAllProducts()
        {
            List<ProductModel> products = _context.Products.Where(r => r.IsActive == true).ToList();

            if (products.Any())
            {
                return _mapper.Map<List<ProductDto>>(products);
            }
            return null;

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
        public ProductDto UpdateProduct(ProductDto dto)
        {
            ProductModel newProduct = _mapper.Map<ProductModel>(dto);

            _context.Products.Update(newProduct);
            _context.SaveChanges();

            dto = _mapper.Map<ProductDto>(newProduct);

            return dto;
        }

        #endregion Public Methods
    }
}