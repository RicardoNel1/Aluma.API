﻿using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IClientPortfolioRepo : IRepoBase<ClientPortfolioDto>
    {
        Task<ClientPortfolioDto> GetClientPortfolio(int clientId);

    }

    public class ClientPortfolioRepo : RepoBase<ClientPortfolioDto>, IClientPortfolioRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IFileStorageRepo _filestorage;
        private readonly IMapper _mapper;

        public ClientPortfolioRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IFileStorageRepo filestorage, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _filestorage = filestorage;
            _mapper = mapper;
        }



        public async Task<ClientPortfolioDto> GetClientPortfolio(int clientId)
        {

            ClientPortfolioDto dto = new ClientPortfolioDto();
            dto.Client = GetClient(clientId);
            dto.Client.User = await GetUserWithAddress(dto.Client.UserId);
            dto.FNA = GetClientFNA(dto.Client.Id);
            dto.Investments = GetInvestments(dto.FNA.Id);
            dto.Retirement = GetRetirement(dto.FNA.Id);
            dto.RetirementPlanning = GetRetirementPlanning(dto.FNA.Id);
            dto.ProvidingDisability = GetProvidingDisability(dto.FNA.Id);
            dto.ProvidingDeath = GetProvidingDeath(dto.FNA.Id);
            dto.ProvidingDread = GetProvidingDread(dto.FNA.Id);
            dto.ShortTermInsurance = GetShortTerm(dto.Client.Id);
            dto.MedicalAid = GetMedical(dto.Client.Id);
            dto.Assumptions = GetAssumptions(dto.FNA.Id);
            dto.Insurance = GetInsurances(dto.FNA.Id);

            return dto;

        }

        private async Task<UserDto> GetUserWithAddress(int userId)
        {
            try
            {
                UserRepo _user = new UserRepo(_context, _host, _config, _filestorage, _mapper);
                UserDto user = await _user.GetUserWithAddress(new() { Id = userId });

                if (user == null)
                    return new();

                return user;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ClientFNADto GetClientFNA(int clientId)
        {
            try
            {
                FNARepo _fna = new FNARepo(_context, _host, _config, _mapper, null);
                ClientFNADto clientFNA = _fna.GetClientFNA(clientId);

                if (clientFNA == null)
                    return new();

                return clientFNA;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ClientDto GetClient(int clientId)
        {
            try
            {
                ClientRepo _clientRepo = new ClientRepo(_context, _host, _config, _mapper, null);
                ClientDto client = _clientRepo.GetClient(new ClientDto() { Id = clientId });

                if (client == null)
                    return new();

                return client;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private List<InvestmentsDto> GetInvestments(int fnaId)
        {
            try
            {
                InvestmentsRepo _investments = new InvestmentsRepo(_context, _host, _config, _mapper);
                List<InvestmentsDto> invertments = _investments.GetInvestments(fnaId);

                if (invertments == null)
                    return new();

                return invertments;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private RetirementSummaryDto GetRetirement(int fnaId)
        {
            try
            {
                RetirementSummaryRepo _retirement = new RetirementSummaryRepo(_context, _host, _config, _mapper);
                RetirementSummaryDto retirementSummary = _retirement.GetRetirementSummary(fnaId);

                if (retirementSummary == null)
                    return new();

                return retirementSummary;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private RetirementPlanningDto GetRetirementPlanning(int fnaId)
        {
            try
            {
                RetirementPlanningRepo _retirement = new RetirementPlanningRepo(_context, _host, _config, _mapper);
                RetirementPlanningDto retirement = _retirement.GetRetirementPlanning(fnaId);

                if (retirement == null)
                    return new();

                return retirement;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ProvidingDisabilitySummaryDto GetProvidingDisability(int fnaId)
        {
            try
            {
                ProvidingDisabilitySummaryRepo _disability = new ProvidingDisabilitySummaryRepo(_context, _host, _config, _mapper);
                ProvidingDisabilitySummaryDto providingOnDisability = _disability.GetProvidingDisabilitySummary(fnaId);

                if (providingOnDisability == null)
                    return new();

                return providingOnDisability;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ProvidingDeathSummaryDto GetProvidingDeath(int fnaId)
        {
            try
            {
                ProvidingDeathSummaryRepo _death = new ProvidingDeathSummaryRepo(_context, _host, _config, _mapper);
                ProvidingDeathSummaryDto providingOnDeath = _death.GetProvidingDeathSummary(fnaId);

                if (providingOnDeath == null)
                    return new();

                return providingOnDeath;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ProvidingOnDreadDiseaseDto GetProvidingDread(int fnaId)
        {
            try
            {
                ProvidingOnDreadDiseaseRepo _dread = new ProvidingOnDreadDiseaseRepo(_context, _host, _config, _mapper);
                ProvidingOnDreadDiseaseDto providingOnDreadDisease = _dread.GetProvidingOnDreadDisease(fnaId);

                if (providingOnDreadDisease == null)
                    return new();

                return providingOnDreadDisease;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private List<ShortTermInsuranceDTO> GetShortTerm(int cliendId)
        {
            try
            {
                ShortTermInsuranceRepo _shortTerm = new ShortTermInsuranceRepo(_context, _host, _config, _mapper);
                List<ShortTermInsuranceDTO> shortTermInsurances = _shortTerm.GetSortTermInsurance(cliendId);

                if (shortTermInsurances == null)
                    return new();

                return shortTermInsurances;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private MedicalAidDTO GetMedical(int clientId)
        {
            try
            {
                MedicalAidRepo _medical = new MedicalAidRepo(_context, _host, _config, _mapper);
                MedicalAidDTO medicalAid = _medical.GetMedicalAid(clientId);

                if (medicalAid == null)
                    return new();

                return medicalAid;
            }
            catch (Exception)
            {
                return new();
            }
        }
               

        private List<InsuranceDto> GetInsurances(int fnaId)
        {
            try
            {
                InsuranceRepo _insurance = new InsuranceRepo(_context, _host, _config, _mapper);
                List<InsuranceDto> insurances = _insurance.GetInsurance(fnaId);

                if (insurances == null)
                    return new();

                return insurances;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private AssumptionsDto GetAssumptions(int fnaId)
        {
            try
            {
                AssumptionsRepo _assumptions = new AssumptionsRepo(_context, _host, _config, _mapper);
                AssumptionsDto assumptions = _assumptions.GetAssumptions(fnaId);

                if (assumptions == null)
                    return new();

                return assumptions;
            }
            catch (Exception)
            {
                return new();
            }
        }

    }
}