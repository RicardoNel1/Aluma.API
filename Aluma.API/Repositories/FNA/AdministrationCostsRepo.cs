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
    public interface IAdministrationCostsRepo : IRepoBase<AdministrationCostsModel>
    {
        AdministrationCostsDto CreateAdministrationCosts(AdministrationCostsDto dto);
        bool DoesAdministrationCostsExist(AdministrationCostsDto dto);
        AdministrationCostsDto GetAdministrationCosts(int clientId);
        AdministrationCostsDto UpdateAdministrationCosts(AdministrationCostsDto dto);


    }

    public class AdministrationCostsRepo : RepoBase<AdministrationCostsModel>, IAdministrationCostsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AdministrationCostsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public AdministrationCostsDto CreateAdministrationCosts(AdministrationCostsDto dto)
        {

            AdministrationCostsModel administrationCosts = _mapper.Map<AdministrationCostsModel>(dto);
            _context.AdministrationCosts.Add(administrationCosts);
            _context.SaveChanges();
            dto = _mapper.Map<AdministrationCostsDto>(administrationCosts);

            return dto;
        }


        public bool DoesAdministrationCostsExist(AdministrationCostsDto dto)
        {
            bool administrationCostsExist = false;
            administrationCostsExist = _context.AdministrationCosts.Where(a => a.FNAId == dto.FNAId).Any();
            return administrationCostsExist;

        }

        public AdministrationCostsDto GetAdministrationCosts(int fnaId)
        {
            AdministrationCostsModel data = _context.AdministrationCosts.Where(c => c.FNAId == fnaId).First();
            return _mapper.Map<AdministrationCostsDto>(data);

        }

        public AdministrationCostsDto UpdateAdministrationCosts(AdministrationCostsDto dto)
        {
            AdministrationCostsModel data = _context.AdministrationCosts.Where(a => a.FNAId == dto.FNAId).FirstOrDefault();                 
            
            data.OtherConveyanceCosts = dto.OtherConveyanceCosts;
            data.AdvertisingCosts = dto.AdvertisingCosts;
            data.RatesAndTaxes = dto.RatesAndTaxes;
            data.OtherAdminDescription = dto.OtherAdminDescription;
            data.OtherAdminCosts = dto.OtherAdminCosts;
            data.TotalEstimatedCosts = dto.TotalEstimatedCosts;

            _context.AdministrationCosts.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<AdministrationCostsDto>(data);
            return dto;

        }



    }
}