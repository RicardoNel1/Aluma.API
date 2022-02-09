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
    public interface IFspMandateRepo : IRepoBase<FSPMandateModel>
    {
        bool DoesApplicationHaveMandate(FSPMandateDto dto);

        FSPMandateDto UpdateFSPMandate(FSPMandateDto dto);

        FSPMandateDto CreateFSPMandate(FSPMandateDto dto);

        FSPMandateDto GetFSPMandate(int clientId);

        bool DeleteFSPMandate(FSPMandateDto dto);
    }

    public class FspMandateRepo : RepoBase<FSPMandateModel>, IFspMandateRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public FspMandateRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public FSPMandateDto CreateFSPMandate(FSPMandateDto dto)
        {
            FSPMandateModel newFsp = _mapper.Map<FSPMandateModel>(dto);

            _context.FspMandates.Add(newFsp);
            _context.SaveChanges();

            dto = _mapper.Map<FSPMandateDto>(newFsp);

            return dto;
        }

        public bool DeleteFSPMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesApplicationHaveMandate(FSPMandateDto dto)
        {
            var fsp = _context.FspMandates.Where(r => r.ClientId == dto.ClientId);
            if (fsp.Any())
            {
                return true;
            }
            return false;
        }

        public FSPMandateDto GetFSPMandate(int clientId)
        {
            var fspModel = _context.FspMandates.Where(r => r.ClientId == clientId);

            if (fspModel.Any())
            {
                return _mapper.Map<FSPMandateDto>(fspModel.First());
            }
            return null;
            
        }

        public FSPMandateDto UpdateFSPMandate(FSPMandateDto dto)
        {
            FSPMandateModel newFsp = _mapper.Map<FSPMandateModel>(dto);

            _context.FspMandates.Update(newFsp);
            _context.SaveChanges();

            dto = _mapper.Map<FSPMandateDto>(newFsp);

            return dto;
        }
    }
}