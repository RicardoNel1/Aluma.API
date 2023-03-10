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
        bool Exists(int fnaId);
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
            Enum.TryParse(accrual.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
            
            clientAccrual.AllocateTo = parsedAllocation;

            _context.Accrual.Add(clientAccrual);
            _context.SaveChanges();
            accrual = _mapper.Map<AccrualDto>(clientAccrual);

            return accrual;
        }

        public AccrualDto DeleteAccrual(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int fnaId)
        {
            return _context.Accrual.Where(a => a.FNAId == fnaId).FirstOrDefault() != null;
        }

        public IQueryable<AccrualModel> FindByCondition(Expression<Func<AccrualModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public AccrualDto GetAccrual(int fnaId)
        {
            AccrualModel accrual = _context.Accrual.Where(c => c.FNAId == fnaId).FirstOrDefault();
            if (accrual == null)
            {
                return new AccrualDto();
            }
            else
            {
                return _mapper.Map<AccrualDto>(accrual);
            }
        }


        public AccrualDto UpdateAccrual(AccrualDto accrual)
        {
            AccrualModel data = _context.Accrual .Where(a => a.FNAId == accrual.FNAId) .FirstOrDefault();
            Enum.TryParse(accrual.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);

            data.ClientAssetsCommencement = accrual.ClientAssetsCommencement;
            data.ClientEstateCurrent = accrual.ClientEstateCurrent;
            data.ClientExcludedValue = accrual.ClientExcludedValue;
            data.ClientLiabilities = accrual.ClientLiabilities;

            data.SpouseAssetsCommencement = accrual.SpouseAssetsCommencement;
            data.SpouseEstateCurrent = accrual.SpouseEstateCurrent;
            data.SpouseLiabilities = accrual.SpouseLiabilities;
            data.SpouseExcludedValue = accrual.SpouseExcludedValue;
            data.AllocateTo = parsedAllocation;
            data.TotalAsAsset = accrual.TotalAsAsset;
            data.TotalAsLiability = accrual.TotalAsLiability;

            data.Offset = accrual.Offset;

            _context.Accrual.Update(data);
            _context.SaveChanges();

            accrual = _mapper.Map<AccrualDto>(accrual);
            return accrual;
        }
    }


}
