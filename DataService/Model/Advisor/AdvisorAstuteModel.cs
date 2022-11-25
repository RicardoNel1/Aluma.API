using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataService.Model.Advisor
{
    [Table("advisor_astute")]
    public class AdvisorAstuteModel : BaseModel
    {
        public int Id { get; set; }
        public int AdvisorId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AdvisorModel Advisor { get; set; }
    }

    public class AdvisorAstuteModelBuilder : IEntityTypeConfiguration<AdvisorAstuteModel>
    {
        public void Configure(EntityTypeBuilder<AdvisorAstuteModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.AdvisorId).IsUnique();

            mb.HasOne(c => c.Advisor)
                .WithOne(c => c.AdvisorAstute)
                .HasForeignKey<AdvisorAstuteModel>(c => c.AdvisorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
