using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface IRecordOfAdviceRepo : IRepoBase<RecordOfAdviceModel>
    {
        RecordOfAdviceDto GetRecordOfAdvice(RecordOfAdviceDto dto);

        bool DoesApplicationHaveRecordOfAdice(RecordOfAdviceDto dto);

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

        public RecordOfAdviceDto CreateRecordOfAdvice(RecordOfAdviceDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteRecordOfAdvice(RecordOfAdviceDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesApplicationHaveRecordOfAdice(RecordOfAdviceDto dto)
        {
            throw new System.NotImplementedException();
        }

        public RecordOfAdviceDto GetRecordOfAdvice(RecordOfAdviceDto dto)
        {
            throw new System.NotImplementedException();
        }

        public RecordOfAdviceDto UpdateRecordOfAdvice(RecordOfAdviceDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}