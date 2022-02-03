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
    public interface ITaxResidencyRepo : IRepoBase<TaxResidencyModel>
    {
        TaxResidencyDto CreateTaxResidency(TaxResidencyDto dto);

        bool DoesTaxResidencyExist(TaxResidencyDto dto);

        TaxResidencyDto GetTaxResidency(int clientId);

        TaxResidencyDto UpdateTaxResidency(TaxResidencyDto dto);

       
    }

    public class TaxResidencyRepo : RepoBase<TaxResidencyModel>, ITaxResidencyRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public TaxResidencyRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public TaxResidencyDto CreateTaxResidency(TaxResidencyDto dto)
        {
            TaxResidencyModel details = _mapper.Map<TaxResidencyModel>(dto);
            _context.TaxResidency.Add(details);
            _context.SaveChanges();
            dto = _mapper.Map<TaxResidencyDto>(details);
            return dto;

        }
               

        public bool DoesTaxResidencyExist(TaxResidencyDto dto)
        {
            throw new System.NotImplementedException();
        }

        public TaxResidencyDto GetTaxResidency(int clientId)
        {
            TaxResidencyModel taxResidencyModel = _context.TaxResidency.Where(r => r.ClientId == clientId).First();
            return _mapper.Map<TaxResidencyDto>(taxResidencyModel);

        }

        public TaxResidencyDto UpdateTaxResidency(TaxResidencyDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}