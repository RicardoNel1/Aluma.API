using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Model
{
    [Table("estate_duty")]
    public class EstateDutyModel : BaseModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public double Abatement { get; set; }
        public double Section4pValue { get; set; }
        public double LimitedRights { get; set; }
        public bool ResidueToSpouse { get; set; }
    }

    public class EstateDutymodelBuilder : IEntityTypeConfiguration<EstateDutyModel>
    {
        public void Configure(EntityTypeBuilder<EstateDutyModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }
}
