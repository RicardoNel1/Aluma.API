using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("products")]
    public class ProductModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Institute { get; set; }
        public string ProductType { get; set; }
        public string ProductCategory { get; set; }
        public bool IsActive { get; set; }

        //to be added
        //variable rates
        //product life span
        //product listing date ranges
    }
    public class ProductModelBuilder : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> mb)
        {
            mb.HasData(new ProductModel()
            {
                Id = 1,
                Name = "Structured Note",
                Description = "",
                Institute = "Standard Bank",
                ProductType = "Investment",
                ProductCategory = "Category 2"
            });

        }
    }

}