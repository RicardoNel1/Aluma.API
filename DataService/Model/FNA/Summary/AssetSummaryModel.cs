﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_summary_assets")]
    public class AssetSummaryModel : BaseModel
    { 
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double TotalAssetsAttractingCGT { get; set; }
        public double TotalAssetsExcemptCGT { get; set; }
        public double TotalLiquidAssets{ get; set; }
        public double TotalAccrual{ get; set; }
        public double TotalLiabilities{ get; set; }
    }
}