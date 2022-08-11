using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Mvc;

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
                ClientDto result = _repo.Client.GetClient(new() { Id = clientId });


                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                UserDto result = await _repo.User.GetUserWithAddress(new UserDto() { Id = userId });

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                AssumptionsDto result = _repo.Assumptions.GetAssumptions(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                ProvidingOnDisabilityDto result = _repo.ProvidingOnDisability.GetProvidingOnDisability(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                ProvidingDisabilitySummaryDto result = _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                ProvidingOnDeathDto result = _repo.ProvidingOnDeath.GetProvidingOnDeath(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                ProvidingDeathSummaryDto result = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                ProvidingOnDreadDiseaseDto result = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                AssetSummaryDto result = _repo.AssetSummary.GetAssetSummary(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                RetirementPlanningDto result = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                RetirementSummaryDto result = _repo.RetirementSummary.GetRetirementSummary(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                EconomyVariablesDto result = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                EstateExpensesDto result = _repo.EstateExpenses.GetEstateExpenses(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                PrimaryResidenceDto result = _repo.PrimaryResidence.GetPrimaryResidence(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                List<InsuranceDto> result = _repo.Insurance.GetInsurance(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
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
                InsuranceSummaryDto result = _repo.InsuranceSummary.GetInsuranceSummary(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
            {
                return new();
            }
        }

        public TaxLumpsumDto GetTaxLumpsum(int fnaId)
        {
            if (fnaId == 0)
                return new();

            try
            {
                TaxLumpsumDto result = _repo.TaxLumpsum.GetTaxLumpsum(fnaId);

                if (result == null)
                    return new();

                return result;
            }
            catch (Exception)
            {
                return new();
            }
        }
    }
}