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
        #region Public Methods

        TaxResidencyDto CreateTaxResidency(TaxResidencyDto dto);

        bool DeleteTaxResidencyItem(int id);

        bool DoesTaxResidencyExist(TaxResidencyDto dto);

        TaxResidencyDto GetTaxResidency(int clientId);

        TaxResidencyDto UpdateTaxResidency(TaxResidencyDto dto);

        #endregion Public Methods
    }

    public class TaxResidencyRepo : RepoBase<TaxResidencyModel>, ITaxResidencyRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public TaxResidencyRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public TaxResidencyDto CreateTaxResidency(TaxResidencyDto dto)
        {

            TaxResidencyModel details = _mapper.Map<TaxResidencyModel>(dto);

            _context.TaxResidency.Add(details);
            _context.SaveChanges();

            dto = _mapper.Map<TaxResidencyDto>(details);

            foreach (var item in dto.TaxResidencyItems)
            {
                ForeignTaxResidencyModel newItem = new ForeignTaxResidencyModel();

                newItem.TaxResidencyId = dto.Id;
                _context.TaxResidencyItems.Add(newItem);

            }
            return dto;

        }


        public bool DeleteTaxResidencyItem(int id)
        {
            ForeignTaxResidencyModel item = _context.TaxResidencyItems.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.TaxResidencyItems.Remove(item);
            _context.SaveChanges();

            return true;
        }

        public bool DoesTaxResidencyExist(TaxResidencyDto dto)
        {
            bool taxResidencyExist = false;
            taxResidencyExist = _context.TaxResidency.Where(a => a.ClientId == dto.ClientId).Any();
            return taxResidencyExist;

        }

        public TaxResidencyDto GetTaxResidency(int clientId)
        {
            TaxResidencyModel taxResidencyModel = _context.TaxResidency.Where(a => a.ClientId == clientId).FirstOrDefault();
            taxResidencyModel.TaxResidencyItems = _context.TaxResidencyItems.Where(a => a.TaxResidencyId == taxResidencyModel.Id).ToList();


            return _mapper.Map<TaxResidencyDto>(taxResidencyModel);

        }

        public TaxResidencyDto UpdateTaxResidency(TaxResidencyDto dto)
        {
            TaxResidencyModel details = _context.TaxResidency.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();

            //set fields to be updated
            details.TaxNumber = dto.TaxNumber;
            details.TaxObligations = dto.TaxObligations;
            details.UsCitizen = dto.UsCitizen;
            details.UsRelinquished = dto.UsRelinquished;
            details.UsOther = dto.UsOther;

            foreach (var item in dto.TaxResidencyItems)
            {

                bool existingItem = _context.TaxResidencyItems.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    ForeignTaxResidencyModel updateItem = _context.TaxResidencyItems.Where(a => a.Id == item.Id).FirstOrDefault();
                    updateItem.Country = item.Country;
                    updateItem.TinNumber = item.TinNumber;
                    updateItem.TinUnavailableReason = item.TinUnavailableReason;
                    _context.TaxResidencyItems.Update(updateItem);

                }
                else
                {
                    ForeignTaxResidencyModel newItem = new ForeignTaxResidencyModel();
                    newItem.TaxResidencyId = dto.Id;
                    newItem.Country = item.Country;
                    newItem.TinNumber = item.TinNumber;
                    newItem.TinUnavailableReason = item.TinUnavailableReason;
                    _context.TaxResidencyItems.Add(newItem);

                }
            }
            _context.TaxResidency.Update(details);
            _context.SaveChanges();
            dto = _mapper.Map<TaxResidencyDto>(details);
            return dto;

        }

        #endregion Public Methods
    }
}