using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aluma.API.Repositories
{

    public interface IAccrualRepo : IRepoBase<AccrualModel>
    {
        AccrualDto CreateAccrual(AccrualDto accrual);
        AccrualDto GetAccrual(int id);
        AccrualDto UpdateAccrual(AccrualDto accrual);
        AccrualDto DeleteAccrual(int id);
    }


    //Does the work
    public class AccrualRepo : RepoBase<AccrualModel>, IAccrualRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AccrualRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public AccrualDto CreateAccrual(AccrualDto accrual)
        {
            AccrualModel clientAccrual = _mapper.Map<AccrualModel>(accrual);
            _context.Accrual.Add(clientAccrual);
            _context.SaveChanges();
            accrual = _mapper.Map<AccrualDto>(clientAccrual);

            return accrual;
        }

        public AccrualDto DeleteAccrual(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AccrualModel> FindByCondition(Expression<Func<AccrualModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public AccrualDto GetAccrual(int clientId)
        {
            AccrualModel accrual = _context.Accrual.Where(c => c.ClientId == clientId).FirstOrDefault();
            return _mapper.Map<AccrualDto>(accrual);
        }

        public AccrualDto UpdateAccrual(AccrualDto accrual)
        {
            throw new NotImplementedException();
        }
    }


}
