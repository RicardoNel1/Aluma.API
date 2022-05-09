using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface ILiabilitiesRepo : IRepoBase<LiabilitiesModel>
    {
        #region Public Methods

        bool DeleteLiabilitiesItem(int id);

        List<LiabilitiesDto> GetLiabilities(int clientId);
        LiabilitiesDto UpdateLiabilities(LiabilitiesDto[] dtoArray);

        #endregion Public Methods
    }

    public class LiabilitiesRepo : RepoBase<LiabilitiesModel>, ILiabilitiesRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public LiabilitiesRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors


        #region Public Methods

        public bool DeleteLiabilitiesItem(int id)
        {
            LiabilitiesModel item = _context.Liabilities.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.Liabilities.Remove(item);
            _context.SaveChanges();

            return true;
        }

        public List<LiabilitiesDto> GetLiabilities(int clientId)
        {
            ICollection<LiabilitiesModel> data = _context.Liabilities.Where(c => c.ClientId == clientId).ToList();
            List<LiabilitiesDto> liabilities = new List<LiabilitiesDto>();

            foreach (var item in data)
            {
                LiabilitiesDto liability = new LiabilitiesDto();

                liability.Id = item.Id;
                liability.ClientId = item.ClientId;
                liability.Description = item.Description;
                liability.Value = item.Value;

                liabilities.Add(liability);

            }

            return liabilities;
        }

        public LiabilitiesDto UpdateLiabilities(LiabilitiesDto[] dtoArray)
        {
            
            foreach (var item in dtoArray)
            {

                bool existingItem = _context.Liabilities.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    LiabilitiesModel updateItem = _context.Liabilities.Where(a => a.Id == item.Id).FirstOrDefault();
                   
                    updateItem.Description = item.Description;
                    updateItem.Value = item.Value;

                    _context.Liabilities.Update(updateItem);

                }
                else
                {
                    LiabilitiesModel newItem = new LiabilitiesModel();

                    newItem.ClientId = item.ClientId;
                    newItem.Description = item.Description;
                    newItem.Value = item.Value;

                    _context.Liabilities.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }

        #endregion Public Methods
    }
}