using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("passports")]
    public class PassportModel : BaseModel
    {
        public ClientModel Client { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string CountryOfIssue { get; set; }
        public string PassportNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class PassportModelBuilder : IEntityTypeConfiguration<PassportModel>
    {
        public void Configure(EntityTypeBuilder<PassportModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();

        }
    }
}