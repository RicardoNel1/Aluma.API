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
        List<LiabilitiesDto> GetLiabilities(int clientId);
        LiabilitiesDto UpdateLiabilities(LiabilitiesDto[] dtoArray);

    }

    public class LiabilitiesRepo : RepoBase<LiabilitiesModel>, ILiabilitiesRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LiabilitiesRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
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



    }
}