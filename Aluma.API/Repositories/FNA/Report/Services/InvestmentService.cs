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
    public interface IInvestmentService
    {
        Task<ReportServiceResult> SetInvestmentDetail(int fnaId);
    }

    public class InvestmentService : BaseReportData, IInvestmentService
    {
        private readonly IGraphService _graph;

        public InvestmentService(IWrapper repo)
        {
            _repo = repo;
            _graph = new GraphService();
        }

        public async Task<ReportServiceResult> SetInvestmentDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }

         private async Task<ReportServiceResult> GetReportData(int fnaId)
        {
                ClientDto client = await GetClient(fnaId);
                UserDto user = await GetUser(client.UserId);

                InvestmentsDto investment = GetInvestments(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, investment));
        }

        private ReportServiceResult ReplaceHtmlPlaceholders(InvestmentReportDto investment)
        {
            string script = string.Empty;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-retirement-planning.html");
            string result = File.ReadAllText(path);

            //result = result.Replace("[IncomeNeed]", investment.IncomeNeed);
            //result = result.Replace("[TermYears]", investment.TermYears);
            //result = result.Replace("[Age]", investment.Age);
            result = result.Replace("[EscalationPercent]", investment.EscalationPercent);
            result = result.Replace("[OutstandingLiabilities]", investment.OutstandingLiabilities);
            //result = result.Replace("[LifeExpectancy]", investment.LifeExpectancy);
            result = result.Replace("[AvailableCapital]", investment.AvailableCapital);
            result = result.Replace("[TotalAvailable]", investment.TotalAvailable);
            result = result.Replace("[IncomeAvailableTotal]", investment.IncomeAvailableTotal);
            result = result.Replace("[RiskRating]", investment.RiskRating);
            result = result.Replace("[InvestmentReturnRate]", investment.InvestmentReturnRate);
            result = result.Replace("[InflationRate]", investment.InflationRate);
            result = result.Replace("[ExhaustionPeriod]", investment.ExhaustionPeriod);
            result = result.Replace("[IncomeNeedsTotal]", investment.IncomeNeedsTotal);
            result = result.Replace("[DescTotalCapital]", investment.DescTotalCapital);
            result = result.Replace("[TotalCapital]", investment.TotalCapital);
            result = result.Replace("[MonthlySavingsRequired]", investment.MonthlySavingsRequired);
            result = result.Replace("[MonthlySavingsEscalating]", investment.MonthlySavingsEscalating);


            if (investment.CapitalGraph != null)
            {
                var graph = _graph.SetGraphHtml(investment.CapitalGraph);
                script += graph.Script;
                result = result.Replace("[CapitalGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[CapitalGraph]", string.Empty);
            }


            if (investment.AnnualGraph != null)
            {
                var graph = _graph.SetGraphHtml(investment.AnnualGraph);
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

        private InvestmentReportDto SetReportFields(ClientDto client, UserDto user, InvestmentsDto investment)
        {            
            double totalcapital = summaryRetirement.TotalAvailable - summaryRetirement.TotalNeeds;
            int exhaustionPeriod = (int)Math.Round(retirement.CapitalAvailable / (retirement.IncomeNeeds * 12));
            double incomeAvailable = retirement.TotalCapitalAvailable - retirement.CapitalAvailable;  //@Justin bad workaround needed because column is empty
            double totalCapitalNeeds = retirement.CapitalNeeds + retirement.OutstandingLiabilities;
            double capitalizedIncomeNeeds = retirement.TotalCapitalNeeds - retirement.OutstandingLiabilities - retirement.CapitalNeeds;

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
                IncomeAvailableTotal = incomeAvailable.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                RiskRating = riskRating == null ? string.Empty : riskRating.ToString(),
                InvestmentReturnRate = EnumConvertions.RiskExpectations(assumptions.RetirementInvestmentRisk).ToString() ?? string.Empty,
                InflationRate = economy_variables.InflationRate.ToString() ?? string.Empty,
                ExhaustionPeriod = exhaustionPeriod.ToString() ?? string.Empty,
                IncomeNeedsTotal = retirement.IncomeNeedsTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                DescTotalCapital = totalcapital < 0 ? "Shortfall" : "Surplus",
                TotalCapitalNeeds = totalCapitalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                CapitalizedIncomeNeeds = capitalizedIncomeNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TotalCapital = totalcapital < 0 ? $"({(totalcapital * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalcapital.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                MonthlySavingsRequired = summaryRetirement.SavingsRequiredPremium < 0 ? $"({(summaryRetirement.SavingsRequiredPremium * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : summaryRetirement.SavingsRequiredPremium.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                MonthlySavingsEscalating = retirement.SavingsEscalation.ToString() ?? string.Empty,
                CapitalGraph = new()
                {
                    Type = GraphType.Column,
                    Name = "Capital position over planning term",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 250,
                    Data = SetCapitalPositionGraph(summaryRetirement.TotalAvailable, summaryRetirement.TotalNeeds, economy_variables.InflationRate, economy_variables.InvestmentReturnRate, assumptions.RetirementAge, assumptions.LifeExpectancy)
                },
                AnnualGraph = new()
                {
                    Type = GraphType.Column,
                    Name = "Annual Income position over planning term",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 250,
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

       

        
    }
}
