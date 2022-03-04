﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("product")]
    public class ProductModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Institute { get; set; }
        public string ProductType { get; set; }
        public string ProductCategory { get; set; }

        //to be added
        //variable rates
        //product life span
        //product listing date ranges
    }    
}