using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("estate_expenses")]
    public class EstateExpensesModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public double AdminCosts { get; set; }
        public double FuneralExpenses { get; set; }
        public double CashBequests { get; set; }
        public double Other { get; set; }
        public double ExecutorsFees { get; set; }
        public double TotalEstateExpenses { get; set; }

    }

    public class EstateExpensesModelBuilder : IEntityTypeConfiguration<EstateExpensesModel>
    {
        public void Configure(EntityTypeBuilder<EstateExpensesModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }

}