using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("bank_details")]
    public class BankDetailsModel : BaseModel
    {
        public ClientModel Client { get; set; }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public string JobID { get; set; }
        public string Name { get; set; }
        public string SearchData { get; set; }
        public string Reference { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public string VerificationType { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountId { get; set; }
        public string IdNumber { get; set; }
        public string Initials { get; set; }
        public string InitialsMatch { get; set; }
        public string Surname { get; set; }
        public string FoundAtBank { get; set; }
        public string AccOpen { get; set; }
        public string OlderThan3Months { get; set; }
        public string TypeCorrect { get; set; }
        public string IdNumberMatch { get; set; }
        public string NamesMatch { get; set; }
        public string AcceptDebits { get; set; }
        public string AcceptCredits { get; set; }
    }

    public class BankDetailsModelBuilder : IEntityTypeConfiguration<BankDetailsModel>
    {
        public void Configure(EntityTypeBuilder<BankDetailsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
            
        }
    }
}