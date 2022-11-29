using DataService.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Model.Client
{
    [Table("financial_providers")]
    public class FinancialProviderModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class FinancialProviderModelBuilder : IEntityTypeConfiguration<FinancialProviderModel>
    {
        public void Configure(EntityTypeBuilder<FinancialProviderModel> mb)
        {
            mb.HasData(new FinancialProviderModel()
            {
                Id = 1,
                Name = "Absa Life",
                Code = "ABSA",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 2,
                Name = "Allan Gray",
                Code = "AG",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 3,
                Name = "Altrisk",
                Code = "ALT",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 4,
                Name = "Liberty Active",
                Code = "CHT",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 5,
                Name = "Discovery Invest",
                Code = "DSI",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 6,
                Name = "Discovery Life",
                Code = "DSL",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 7,
                Name = "Liberty Life",
                Code = "LIB",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 8,
                Name = "Momentum",
                Code = "MOM",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 9,
                Name = "Momentum Wealth",
                Code = "MOMW",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 10,
                Name = "Nedgroup Life",
                Code = "NGL",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 11,
                Name = "Galaxy Portfolio Services",
                Code = "OMGP",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 12,
                Name = "Old Mutual",
                Code = "OMU",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 13,
                Name = "Old Mutual Unit Trusts",
                Code = "OUT",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 14,
                Name = "PPS",
                Code = "PPS",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 15,
                Name = "Sanlam Collective Investments",
                Code = "SET",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 16,
                Name = "Sanlam",
                Code = "SLM",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 17,
                Name = "Sanlam Namibia",
                Code = "SLMNA",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 18,
                Name = "Stanlib",
                Code = "STLB",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 19,
                Name = "FMI",
                Code = "FMI",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 20,
                Name = "Momentum Employee Benefit",
                Code = "MOME",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 21,
                Name = "Sanlam Employee Benefit",
                Code = "SLME",
            });
            mb.HasData(new FinancialProviderModel()
            {
                Id = 22,
                Name = "Astute Medical Aid Service",
                Code = "AMAS",
            });

        }
    }
}
