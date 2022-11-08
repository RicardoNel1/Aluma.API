using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_id_details")]
    public class IDVModel : BaseModel
    {
        public ClientModel Client { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TraceId { get; set; }
        public string IdNumber { get; set; }
        public string HomeAffairsIdNo { get; set; }
        public string IdNoMatchStatus { get; set; }
        public string IdBookIssuedDate { get; set; }
        public string IdType { get; set; }
        public string IdCardInd { get; set; }
        public string IdCardDate { get; set; }
        public string IdBlocked { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string CountryofBirth { get; set; }
        public string DeceasedStatus { get; set; }
        public string DeceasedDate { get; set; }
        public string DeathPlace { get; set; }
        public string CauseOfDeath { get; set; }
        public string MaritalStatus { get; set; }
        public string MarriageDate { get; set; }
    }

    public class IDVModelBuilder : IEntityTypeConfiguration<IDVModel>
    {
        public void Configure(EntityTypeBuilder<IDVModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
            
        }
    }
}