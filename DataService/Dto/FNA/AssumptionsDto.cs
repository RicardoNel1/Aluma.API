﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class AssumptionsDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RetirementAge { get; set; }
        public double CurrentNetIncome { get; set; }
        public string DeathInvestmentRisk { get; set; }
        public string DisabilityInvestmentRisk { get; set; }
    }
}