using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IRecordOfAdviceRepo : IRepoBase<RecordOfAdviceModel>
    {
        RecordOfAdviceDto GetRecordOfAdvice(int applicationId);

        bool DoesApplicationHaveRecordOfAdice(int applicationId);

        RecordOfAdviceDto CreateRecordOfAdvice(RecordOfAdviceDto dto);

        RecordOfAdviceDto UpdateRecordOfAdvice(RecordOfAdviceDto dto);

        bool DeleteRecordOfAdvice(RecordOfAdviceDto dto);
    }

    public class RecordOfAdviceRepo : RepoBase<RecordOfAdviceModel>, IRecordOfAdviceRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RecordOfAdviceRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _host = host;
            _config = config;
            _mapper = mapper;
            _context = databaseContext;
        }

        public bool DoesApplicationHaveRecordOfAdice(int applicationId)
        {
            var roa = _context.RecordOfAdvice.Where(r => r.ApplicationId == applicationId);
            if (roa.Any())
            {
                return true;
            }
            return false;
        }

        public RecordOfAdviceDto GetRecordOfAdvice(int applicationId)
        {
            var roa = _context.RecordOfAdvice.Where(r => r.ApplicationId == applicationId);

            if (roa.Any())
            {
                RecordOfAdviceDto result = _mapper.Map<RecordOfAdviceDto>(roa.Include(r => r.SelectedProducts).First());

                foreach (var product in result.SelectedProducts)
                {
                    product.ProductName = _context.Products.First(p => p.Id == product.ProductId).Name;
                }

                return result;
            }
            return null;
        }

        public RecordOfAdviceDto CreateRecordOfAdvice(RecordOfAdviceDto dto)
        {
            RecordOfAdviceModel newRoa = _mapper.Map<RecordOfAdviceModel>(dto);

            _context.RecordOfAdvice.Add(newRoa);
            _context.SaveChanges();

            dto = _mapper.Map<RecordOfAdviceDto>(newRoa);

            return dto; 
        }

        public RecordOfAdviceDto UpdateRecordOfAdvice(RecordOfAdviceDto dto)
        {
            RecordOfAdviceModel newRoa = _mapper.Map<RecordOfAdviceModel>(dto);

            _context.RecordOfAdvice.Update(newRoa);
            _context.SaveChanges();

            dto = _mapper.Map<RecordOfAdviceDto>(newRoa);

            return dto;
        }

        public bool DeleteRecordOfAdvice(RecordOfAdviceDto dto)
        {
            throw new System.NotImplementedException();
        }

        internal void GenerateRecordOfAdvice(UserModel user, AdvisorModel ad, RiskProfileModel r)
        {
            throw new NotImplementedException();
        }
    }
}