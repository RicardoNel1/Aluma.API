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
        #region Public Methods

        AdministrationCostsDto CreateAdministrationCosts(AdministrationCostsDto dto);
        bool DoesAdministrationCostsExist(AdministrationCostsDto dto);
        AdministrationCostsDto GetAdministrationCosts(int clientId);
        AdministrationCostsDto UpdateAdministrationCosts(AdministrationCostsDto dto);

        #endregion Public Methods


    }

    public class AdministrationCostsRepo : RepoBase<AdministrationCostsModel>, IAdministrationCostsRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public AdministrationCostsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

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
            administrationCostsExist = _context.AdministrationCosts.Where(a => a.ClientId == dto.ClientId).Any();
            return administrationCostsExist;

        }

        public AdministrationCostsDto GetAdministrationCosts(int clientId)
        {
            AdministrationCostsModel data = _context.AdministrationCosts.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<AdministrationCostsDto>(data);

        }

        public AdministrationCostsDto UpdateAdministrationCosts(AdministrationCostsDto dto)
        {
            AdministrationCostsModel data = _context.AdministrationCosts.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();

            data.OtherFixedProperty = dto.OtherFixedProperty;
            data.OtherFixedPropertyValue = dto.OtherFixedPropertyValue;

            data.OtherConveyanceCosts = dto.OtherConveyanceCosts;
            data.AdvertisingCosts = dto.AdvertisingCosts;
            data.RatesAndTaxes = dto.RatesAndTaxes;
            data.OtherAdministrationCosts = dto.OtherAdministrationCosts;
            data.OtherAdministrationCostsValue = dto.OtherAdministrationCostsValue;
            data.TotalEstimatedCosts = dto.TotalEstimatedCosts;

            _context.AdministrationCosts.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<AdministrationCostsDto>(data);
            return dto;

        }

        #endregion Public Methods



    }
}