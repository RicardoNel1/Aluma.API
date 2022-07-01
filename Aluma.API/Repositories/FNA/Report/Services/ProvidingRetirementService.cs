using Aluma.API.Helpers.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using DataService.Migrations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IProvidingRetirementService
    {
        Task<ReportServiceResult> SetRetirementDetail(int fnaId);
    }

    public class ProvidingRetirementService : BaseReportData, IProvidingRetirementService
    {
        private readonly IGraphService _graph;

        public ProvidingRetirementService(IWrapper repo)
        {
            _repo = repo;
            _graph = new GraphService();
        }

        private ReportServiceResult ReplaceHtmlPlaceholders(RetirementPlanningReportDto retirement)
        {
            string script = string.Empty;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-retirement-planning.html");
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
            result = result.Replace("[InvestmentReturnRate]", EnumConvertions.RiskExpectations(retirement.InvestmentReturnRate).ToString());
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
                IncomeNeed = retirement.IncomeNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TermYears = retirement.NeedsTerm_Years.ToString() ?? string.Empty,
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                EscalationPercent = economy_variables.InflationRate.ToString() ?? string.Empty,
                RetirementAge = assumptions.RetirementAge.ToString() ?? string.Empty,
                TotalNeeds = summaryRetirement.TotalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                YearsBeforeRetirement = assumptions.YearsTillRetirement.ToString() ?? string.Empty,
                YearsAfterRetirement = assumptions.YearsAfterRetirement.ToString() ?? string.Empty,
                CapitalNeeds = retirement.CapitalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                OutstandingLiabilities = retirement.OutstandingLiabilities.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                LifeExpectancy = assumptions.LifeExpectancy.ToString() ?? string.Empty,
                AvailableCapital = retirement.CapitalAvailable.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TotalAvailable = summaryRetirement.TotalAvailable.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                IncomeAvailableTotal = retirement.IncomeAvailableTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                RiskRating = riskRating == null ? string.Empty : riskRating.ToString(),
                InvestmentReturnRate = EnumConvertions.RiskExpectations(assumptions.RetirementInvestmentRisk).ToString() ?? string.Empty,
                InflationRate = economy_variables.InflationRate.ToString() ?? string.Empty,
                ExhaustionPeriod = exhaustionPeriod.ToString() ?? string.Empty,
                IncomeNeedsTotal = retirement.IncomeNeedsTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                DescTotalCapital = totalcapital < 0 ? "Shortfall" : "Surplus",
                TotalCapital = totalcapital < 0 ? $"({(totalcapital * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalcapital.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                MonthlySavingsRequired = summaryRetirement.SavingsRequiredPremium < 0 ? $"({(summaryRetirement.SavingsRequiredPremium * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : summaryRetirement.SavingsRequiredPremium.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                MonthlySavingsEscalating = retirement.SavingsEscalation.ToString() ?? string.Empty,
                CapitalGraph = new()
                {
                    Type = GraphType.Column,
                    Name = "Capital position over planning term",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 260,
                    Data = SetCapitalPositionGraph(summaryRetirement.TotalAvailable, summaryRetirement.TotalNeeds, economy_variables.InflationRate, economy_variables.InvestmentReturnRate, assumptions.RetirementAge, assumptions.LifeExpectancy)
                },
                AnnualGraph = new()
                {
                    Type = GraphType.Column,
                    Name = "Annual Income position over planning term",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 260,
                    Data = SetAnnualPositionGraph(summaryRetirement.TotalAvailable, summaryRetirement.TotalNeeds, economy_variables.InflationRate, economy_variables.InvestmentReturnRate, assumptions.RetirementAge, assumptions.LifeExpectancy)
                },
            };
        }

        private static List<string> SetCapitalPositionGraph(double totalAvailable, double totalNeeds, double escalation, double investment, int retirementAge, int lifeExpect)
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

                var calcValue = Math.Round(annualTotal - (needValue * 12));
                annualTotal = calcValue;

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

        private static List<string> SetAnnualPositionGraph(double totalAvailable, double totalNeeds, double escalation, double investment, int retirementAge, int lifeExpect)
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

                var calcValue = Math.Round(annualTotal - (needValue * 12));
                annualTotal = calcValue;

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
                ClientDto client = await GetClient(fnaId);
                UserDto user = await GetUser(client.UserId);

                AssumptionsDto assumptions = GetAssumptions(fnaId);
                RetirementPlanningDto retirement = GetRetirementPlanning(fnaId);
                RetirementSummaryDto summaryRetirement = GetRetirementSummary(fnaId);
                EconomyVariablesDto economy_variables = GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, retirement, summaryRetirement, economy_variables));
        }

        public async Task<ReportServiceResult> SetRetirementDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }
    }
}
