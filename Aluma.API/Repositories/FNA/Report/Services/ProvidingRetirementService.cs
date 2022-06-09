using Aluma.API.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using DataService.Migrations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IProvidingRetirementService
    {
        Task<ReportServiceResult> SetRetirementDetail(int fnaId);
    }

    public class ProvidingRetirementService : IProvidingRetirementService
    {
        private readonly IWrapper _repo;
        private readonly IGraphService _graph;

        public ProvidingRetirementService(IWrapper repo)
        {
            _repo = repo;
            _graph = new GraphService();
        }

        private ReportServiceResult ReplaceHtmlPlaceholders(RetirementPlanningReportDto retirement)
        {
            string script = string.Empty;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-retirement-planning.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[IncomeNeed]", retirement.IncomeNeed);
            result = result.Replace("[TermYears]", retirement.TermYears);
            result = result.Replace("[Age]", retirement.Age);
            result = result.Replace("[EscalationPercent]", retirement.EscalationPercent);
            result = result.Replace("[RetirementAge]", retirement.RetirementAge);
            result = result.Replace("[TotalNeeds]", retirement.TotalNeeds);
            result = result.Replace("[YearsBeforeRetirement]", retirement.YearsBeforeRetirement);
            result = result.Replace("[YearsAfterRetirement]", retirement.YearsAfterRetirement);
            result = result.Replace("[CapitalNeeds]", retirement.CapitalNeeds);
            result = result.Replace("[OutstandingLiabilities]", retirement.OutstandingLiabilities);
            result = result.Replace("[LifeExpectancy]", retirement.LifeExpectancy);
            result = result.Replace("[AvailableCapital]", retirement.AvailableCapital);
            result = result.Replace("[TotalAvailable]", retirement.TotalAvailable);
            result = result.Replace("[IncomeAvailableTotal]", retirement.IncomeAvailableTotal);
            result = result.Replace("[RiskRating]", retirement.RiskRating);
            result = result.Replace("[InvestmentReturnRate]", retirement.InvestmentReturnRate);
            result = result.Replace("[InflationRate]", retirement.InflationRate);
            result = result.Replace("[ExhaustionPeriod]", retirement.ExhaustionPeriod);
            result = result.Replace("[IncomeNeedsTotal]", retirement.IncomeNeedsTotal);
            result = result.Replace("[DescTotalCapital]", retirement.DescTotalCapital);
            result = result.Replace("[TotalCapital]", retirement.TotalCapital);
            result = result.Replace("[MonthlySavingsRequired]", retirement.MonthlySavingsRequired);
            result = result.Replace("[MonthlySavingsEscalating]", retirement.MonthlySavingsEscalating);


            if (retirement.CapitalGraph != null)
            {
                var graph = _graph.SetGraphHtml(retirement.CapitalGraph);
                script += graph.Script;
                result = result.Replace("[CapitalGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[CapitalGraph]", string.Empty);
            }


            if (retirement.AnnualGraph != null)
            {
                var graph = _graph.SetGraphHtml(retirement.AnnualGraph);
                script += graph.Script;
                result = result.Replace("[AnnualGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[AnnualGraph]", string.Empty);
            }

            return new()
            {
                Html = result,
                Script = script
            }; ;

        }

        private RetirementPlanningReportDto SetReportFields(ClientDto client, UserDto user,
                                                        AssumptionsDto assumptions,
                                                        RetirementPlanningDto retirement,
                                                        RetirementSummaryDto summaryRetirement,
                                                        EconomyVariablesDto economy_variables)
        {
            RiskRatingsEnum? riskRating = null;
            if (Enum.IsDefined(typeof(RiskRatingsEnum), assumptions.RetirementInvestmentRisk))
                riskRating = ((RiskRatingsEnum)Enum.Parse(typeof(RiskRatingsEnum), assumptions.RetirementInvestmentRisk));

            double totalcapital = summaryRetirement.TotalAvailable - summaryRetirement.TotalNeeds;
            int exhaustionPeriod = (int)Math.Round(retirement.CapitalAvailable / (retirement.IncomeNeeds * 12));

            return new RetirementPlanningReportDto()
            {
                IncomeNeed = retirement.IncomeNeeds.ToString(),
                TermYears = retirement.NeedsTerm_Years.ToString(),
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                EscalationPercent = economy_variables.InflationRate.ToString(),
                RetirementAge = assumptions.RetirementAge.ToString(),
                TotalNeeds = summaryRetirement.TotalNeeds.ToString(),
                YearsBeforeRetirement = assumptions.YearsTillRetirement.ToString(),
                YearsAfterRetirement = assumptions.YearsAfterRetirement.ToString(),
                CapitalNeeds = retirement.CapitalNeeds.ToString(),
                OutstandingLiabilities = retirement.OutstandingLiabilities.ToString(),
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                AvailableCapital = retirement.CapitalAvailable.ToString(),
                TotalAvailable = summaryRetirement.TotalAvailable.ToString(),
                IncomeAvailableTotal = retirement.IncomeAvailableTotal.ToString(),
                RiskRating = riskRating == null ? string.Empty : riskRating.ToString(),
                InvestmentReturnRate = EnumConvertions.RiskExpectations(assumptions.RetirementInvestmentRisk).ToString(),
                InflationRate = economy_variables.InflationRate.ToString(),
                ExhaustionPeriod = exhaustionPeriod.ToString(),
                IncomeNeedsTotal = retirement.IncomeNeedsTotal.ToString(),
                DescTotalCapital = totalcapital < 0 ? "Shortfall" : "Surplus",
                TotalCapital = totalcapital < 0 ? $"({totalcapital * -1})" : totalcapital.ToString(),
                MonthlySavingsRequired = summaryRetirement.SavingsRequiredPremium.ToString(),
                MonthlySavingsEscalating = retirement.SavingsEscalation.ToString(),
                CapitalGraph = new()
                {
                    Type = GraphType.Column,
                    Name = "Capital position over planning term",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 300,
                    Data = SetCapitalPositionGraph(summaryRetirement.TotalAvailable, summaryRetirement.TotalNeeds, economy_variables.InflationRate, economy_variables.InvestmentReturnRate, assumptions.RetirementAge, assumptions.LifeExpectancy)
                },
                AnnualGraph = new()
                {
                    Type = GraphType.Column,
                    Name = "Annual Income position over planning term",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 300,
                    Data = SetAnnualPositionGraph(summaryRetirement.TotalAvailable, summaryRetirement.TotalNeeds, economy_variables.InflationRate, economy_variables.InvestmentReturnRate, assumptions.RetirementAge, assumptions.LifeExpectancy)
                },
            };
        }

        private List<string> SetCapitalPositionGraph(double totalAvailable, double totalNeeds, double escalation, double investment, int retirementAge, int lifeExpect)
        {
            List<string> result = new List<string>();
            List<double> totals = new List<double>();

            int yearsAftRetirement = lifeExpect - retirementAge;
            double needValue = totalNeeds;
            double annualTotal = totalAvailable;

            for (int year = 0; year <= yearsAftRetirement; year++)
            {
                if (year != 0)
                {
                    needValue = Math.Round(needValue + (needValue * (escalation / 100)));
                }

                //for (int month = 0; month < 12; month++)
                //{
                var calcValue = Math.Round(annualTotal - (needValue * 12));





                //var calValue = Math.Round((annualTotal - needValue) * (1 + (investment / 1200)));
                annualTotal = calcValue;
                //}

                totals.Add(annualTotal);
            }

            for (int i = retirementAge; i <= lifeExpect; i++)
            {
                if (totals.Count >= (i - retirementAge))
                {
                    result.Add($"{i},{totals[i - retirementAge]}");
                }
            }

            return result;
        }

        private List<string> SetAnnualPositionGraph(double totalAvailable, double totalNeeds, double escalation, double investment, int retirementAge, int lifeExpect)
        {
            List<string> result = new List<string>();
            List<double> totals = new List<double>();

            int yearsAftRetirement = lifeExpect - retirementAge;
            double needValue = totalNeeds;
            double annualTotal = totalAvailable;

            for (int year = 0; year <= yearsAftRetirement; year++)
            {
                if (year != 0)
                {
                    needValue = Math.Round(needValue + (needValue * (escalation / 100)));
                }

                //for (int month = 0; month < 12; month++)
                //{
                var calcValue = Math.Round(annualTotal - (needValue * 12));





                //var calValue = Math.Round((annualTotal - needValue) * (1 + (investment / 1200)));
                annualTotal = calcValue;
                //}

                totals.Add(annualTotal);
            }

            for (int i = retirementAge; i <= lifeExpect; i++)
            {
                if (totals.Count >= (i - retirementAge))
                {
                    result.Add($"{i},{totals[i - retirementAge]}");
                }
            }

            return result;
        }

        private async Task<ReportServiceResult> GetReportData(int fnaId)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

                AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);
                RetirementPlanningDto retirement = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);
                RetirementSummaryDto summaryRetirement = _repo.RetirementSummary.GetRetirementSummary(fnaId);
                EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, retirement, summaryRetirement, economy_variables));
            }
            catch (Exception)
            {
                return new ReportServiceResult();
            }
        }

        public async Task<ReportServiceResult> SetRetirementDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }
    }
}
