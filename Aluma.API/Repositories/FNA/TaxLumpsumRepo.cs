using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface ITaxLumpsumRepo : IRepoBase<TaxLumpsumModel>
    {
        TaxLumpSumDto CreateTaxLumpsum(TaxLumpSumDto dto);
        TaxLumpSumDto GetAccrual(int id);
        TaxLumpSumDto UpdateAccrual(TaxLumpSumDto accrual);
        TaxLumpSumDto DeleteAccrual(int id);
        bool Exists(int clientId);
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

        public TaxLumpSumDto DeleteAccrual(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(int clientId)
        {
            throw new System.NotImplementedException();
        }

        public TaxLumpSumDto GetAccrual(int id)
        {
            throw new System.NotImplementedException();
        }

        public TaxLumpSumDto UpdateAccrual(TaxLumpSumDto accrual)
        {
            throw new System.NotImplementedException();
        }
    }
}
