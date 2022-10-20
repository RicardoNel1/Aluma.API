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

                List<InvestmentsDto> investments = GetInvestments(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(user, investments));
        }

        private InvestmentReportDto SetReportFields(UserDto user, List<InvestmentsDto> investments)
        {
            //List<InvestmentReportDto>
            //double totalcapital = summaryRetirement.TotalAvailable - summaryRetirement.TotalNeeds;
            //int exhaustionPeriod = (int)Math.Round(retirement.CapitalAvailable / (retirement.IncomeNeeds * 12));
            //double incomeAvailable = retirement.TotalCapitalAvailable - retirement.CapitalAvailable;
            //double totalCapitalNeeds = retirement.CapitalNeeds + retirement.OutstandingLiabilities;
            //double capitalizedIncomeNeeds = retirement.TotalCapitalNeeds - retirement.OutstandingLiabilities - retirement.CapitalNeeds;

            //ReportList = new List<InvestmentReportDto>();
            //foreach (var items in investments)
            //{
            //    ReportList..Add(items.Value.ToString());
            //    //RepDto.InitialValue.Add("Test");
            //    //for (int i = 0; i < investments.Count; i++)
            //    //{
            //    //    ReportList.[i].Value = items.Value;

            //    //}

            //};
            
            return new InvestmentReportDto()
            {
                Investments = investments,
                
                InvestmentPieGraph = new()
                {
                    Type = GraphType.Pie,
                    Name = "Phill",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 250,
                    Data = SetInvestmentPieGraphData(investments)
                },

                InvestmentLineGraph = new()
                {
                    Type = GraphType.Line,
                    Name = "Joe",
                    XaxisHeader = "Capital",
                    //YaxisHeader = "Amount",
                    Height = 250,
                    Data = SetInvestmentLineGraphData(investments)
                },






            };
            //{
            //foreach (var items in investments) {
            //InitialValue.Add()
            //};

            //return new InvestmentReportDto {
            //    InitialValue = RepDto.InitialValue
            //InitialValue = investments[0].Value.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //IncomeNeed = retirement.IncomeNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //TermYears = retirement.NeedsTerm_Years.ToString() ?? string.Empty,
            //Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
            //EscalationPercent = economy_variables.InflationRate.ToString() ?? string.Empty,
            //RetirementAge = assumptions.RetirementAge.ToString() ?? string.Empty,
            //TotalNeeds = summaryRetirement.TotalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //YearsBeforeRetirement = assumptions.YearsTillRetirement.ToString() ?? string.Empty,
            //YearsAfterRetirement = assumptions.YearsAfterRetirement.ToString() ?? string.Empty,
            //CapitalNeeds = retirement.CapitalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //OutstandingLiabilities = retirement.OutstandingLiabilities.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //LifeExpectancy = assumptions.LifeExpectancy.ToString() ?? string.Empty,
            //AvailableCapital = retirement.CapitalAvailable.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //TotalAvailable = summaryRetirement.TotalAvailable.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //IncomeAvailableTotal = incomeAvailable.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //RiskRating = riskRating == null ? string.Empty : riskRating.ToString(),
            //InvestmentReturnRate = EnumConvertions.RiskExpectations(assumptions.RetirementInvestmentRisk).ToString() ?? string.Empty,
            //InflationRate = economy_variables.InflationRate.ToString() ?? string.Empty,
            //ExhaustionPeriod = exhaustionPeriod.ToString() ?? string.Empty,
            //IncomeNeedsTotal = retirement.IncomeNeedsTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //DescTotalCapital = totalcapital < 0 ? "Shortfall" : "Surplus",
            //TotalCapitalNeeds = totalCapitalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //CapitalizedIncomeNeeds = capitalizedIncomeNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //TotalCapital = totalcapital < 0 ? $"({(totalcapital * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalcapital.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
            //MonthlySavingsRequired = summaryRetirement.SavingsRequiredPremium < 0 ? $"({(summaryRetirement.SavingsRequiredPremium * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : summaryRetirement.SavingsRequiredPremium.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
            //MonthlySavingsEscalating = retirement.SavingsEscalation.ToString() ?? string.Empty,
            
                //AnnualGraph = new()
                //{
                //    Type = GraphType.Column,
                //    Name = "Annual Income position over planning term",
                //    XaxisHeader = "Capital",
                //    YaxisHeader = "Amount",
                //    Height = 250,
                //    Data = SetAnnualPositionGraph(summaryRetirement.TotalAvailable, summaryRetirement.TotalNeeds, economy_variables.InflationRate, economy_variables.InvestmentReturnRate, assumptions.RetirementAge, assumptions.LifeExpectancy)
                //},
                //};
            //};
        }

        private ReportServiceResult ReplaceHtmlPlaceholders(InvestmentReportDto investment)
        {
            string script = string.Empty;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-investments.html");
            string result = File.ReadAllText(path);

            //result = result.Replace("[Value0]", "");
            //result = result.Replace("[Value1]", "");
            //result = result.Replace("[Value2]", "");
            //result = result.Replace("[Value3]", "");
            //result = result.Replace("[Value4]", "");
            //result = result.Replace("[Value5]", "");
            //result = result.Replace("[Value6]", "");
            //result = result.Replace("[Value7]", "");

            //result = result.Replace("[Desc0]", "");
            //result = result.Replace("[Desc1]", "");
            //result = result.Replace("[Desc2]", "");
            //result = result.Replace("[Desc3]", "");
            //result = result.Replace("[Desc4]", "");
            //result = result.Replace("[Desc5]", "");
            //result = result.Replace("[Desc6]", "");
            //result = result.Replace("[Desc7]", "");

            for (int i = 0; i < investment.Investments.Count; i++)
            //for (int i = 0; i < 8; i++)
            {
                result = result.Replace($"[Desc{i}]", investment.Investments[i].Description.ToString());
                result = result.Replace($"[Value{i}]", investment.Investments[i].Value.ToString());
            }

            if(investment.Investments.Count < 9)
            {
                for (int i = investment.Investments.Count; i < 9; i++)
                //for (int i = 0; i < 8; i++)
                {
                    result = result.Replace($"[Desc{i}]", "");
                    result = result.Replace($"[Value{i}]", "");
                }
            }



            //for (int n = 0; n < investment.Investments.Count; n++) {
            //    result = result.Replace($"[Value{n}]", investment.Investments[n].Value.ToString());
            //}



            //result = result.Replace("[Value1]", investment.Investments[1].Value.ToString()) ?? "";
            //result = result.Replace("[Value2]", investment.Investments[2].Value.ToString());
            //result = result.Replace("[Value3]", investment.Investments[3].Value.ToString());
            //result = result.Replace("[Value4]", investment.Investments[4].Value.ToString());


            //result = result.Replace("[IncomeNeed]", investment.IncomeNeed);
            //result = result.Replace("[TermYears]", investment.TermYears);
            //result = result.Replace("[Age]", investment.Age);
            //result = result.Replace("[EscalationPercent]", investment.EscalationPercent);
            //result = result.Replace("[OutstandingLiabilities]", investment.OutstandingLiabilities);
            ////result = result.Replace("[LifeExpectancy]", investment.LifeExpectancy);
            //result = result.Replace("[AvailableCapital]", investment.AvailableCapital);
            //result = result.Replace("[TotalAvailable]", investment.TotalAvailable);
            //result = result.Replace("[IncomeAvailableTotal]", investment.IncomeAvailableTotal);
            //result = result.Replace("[RiskRating]", investment.RiskRating);
            //result = result.Replace("[InvestmentReturnRate]", investment.InvestmentReturnRate);
            //result = result.Replace("[InflationRate]", investment.InflationRate);
            //result = result.Replace("[ExhaustionPeriod]", investment.ExhaustionPeriod);
            //result = result.Replace("[IncomeNeedsTotal]", investment.IncomeNeedsTotal);
            //result = result.Replace("[DescTotalCapital]", investment.DescTotalCapital);
            //result = result.Replace("[TotalCapital]", investment.TotalCapital);
            //result = result.Replace("[MonthlySavingsRequired]", investment.MonthlySavingsRequired);
            //result = result.Replace("[MonthlySavingsEscalating]", investment.MonthlySavingsEscalating);


            if (investment.InvestmentPieGraph != null)
            {
                var graph = _graph.SetGraphHtml(investment.InvestmentPieGraph);
                script += graph.Script;
                result = result.Replace("[InvestmentPieGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[InvestmentPieGraph]", string.Empty);
            }


            if (investment.InvestmentLineGraph != null)
            {
                var graph = _graph.SetGraphHtml(investment.InvestmentLineGraph);
                script += graph.Script;
                result = result.Replace("[InvestmentLineGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[InvestmentLineGraph]", string.Empty);
            }

            return new()
            {
                Html = result,
                Script = script
            }; ;

        }



        private static List<string> SetInvestmentPieGraphData(List<InvestmentsDto> investments)
        {
            List<string> investmentPieList = new List<string>();

            foreach (var items in investments)
            {
                investmentPieList.Add( $"{items.Description}, { items.Value}");
                
            };
            return investmentPieList;
        }

        private static List<string> SetInvestmentLineGraphData(List<InvestmentsDto> investments)
        {
            List<string> investmentLineList = new List<string>();

            foreach (var items in investments)
            {
                investmentLineList.Add($"{items.Description}, { items.Value}");

            };
            return investmentLineList;
        }



    }
}
