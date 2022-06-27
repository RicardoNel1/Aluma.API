using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aluma.API.RepoWrapper;
using DataService.Dto;

namespace Aluma.API.Repositories.FNA.Report.Services.Base
{
    public class BaseReportData
    {
        public IWrapper _repo;

        public async Task<ClientDto> GetClient(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                return _repo.Client.GetClient(new() { Id = clientId });
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public async Task<UserDto> GetUser(int userId)
        {
            if (userId == 0)
                return new();

            try
            {
                return await _repo.User.GetUserWithAddress(new UserDto() { Id = userId });
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public AssumptionsDto GetAssumptions(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.Assumptions.GetAssumptions(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public ProvidingOnDisabilityDto GetProvidingOnDisability(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.ProvidingOnDisability.GetProvidingOnDisability(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public ProvidingDisabilitySummaryDto GetProvidingDisabilitySummary(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public ProvidingOnDeathDto GetProvidingOnDeath(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.ProvidingOnDeath.GetProvidingOnDeath(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public ProvidingDeathSummaryDto GetProvidingDeathSummary(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public ProvidingOnDreadDiseaseDto GetProvidingOnDreadDisease(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public AssetSummaryDto GetAssetSummary(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.AssetSummary.GetAssetSummary(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public RetirementPlanningDto GetRetirementPlanning(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.RetirementPlanning.GetRetirementPlanning(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public RetirementSummaryDto GetRetirementSummary(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.RetirementSummary.GetRetirementSummary(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public EconomyVariablesDto GetEconomyVariablesSummary(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public EstateExpensesDto GetEstateExpenses(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.EstateExpenses.GetEstateExpenses(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public PrimaryResidenceDto GetPrimaryResidence(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.PrimaryResidence.GetPrimaryResidence(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public List<InsuranceDto> GetInsurance(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.Insurance.GetInsurance(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public InsuranceSummaryDto GetInsuranceSummary(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                return _repo.InsuranceSummary.GetInsuranceSummary(fnaId);
            }
            catch (Exception ex)
            {
                return new();
            }
        }

    }
}