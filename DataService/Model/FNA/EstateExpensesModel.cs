using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_estate_expenses")]
    public class EstateExpensesModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
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