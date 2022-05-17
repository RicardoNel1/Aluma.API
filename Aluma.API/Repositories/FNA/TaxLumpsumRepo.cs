using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface ITaxLumpsumRepo : IRepoBase<TaxLumpsumModel>
    {
        TaxLumpSumDto CreateTaxLumpsum(TaxLumpSumDto dto);
        TaxLumpSumDto GetTaxLumpsum(int id);
        TaxLumpSumDto UpdateTaxLumpsum(TaxLumpSumDto accrual);
        TaxLumpSumDto DeleteTaxLumpsum(int id);
    }

    public class TaxLumpsumRepo : RepoBase<TaxLumpsumModel>, ITaxLumpsumRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public TaxLumpsumRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public TaxLumpSumDto CreateTaxLumpsum(TaxLumpSumDto dto)
        {
            throw new System.NotImplementedException();
        }

        public TaxLumpSumDto GetTaxLumpsum(int id)
        {   
            return _mapper.Map<TaxLumpSumDto>(_context.TaxLumpsum.Where(c => c.ClientId == id).FirstOrDefault());
        }

        public TaxLumpSumDto UpdateTaxLumpsum(TaxLumpSumDto accrual)
        {
            throw new System.NotImplementedException();
        }

        public TaxLumpSumDto DeleteTaxLumpsum(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
