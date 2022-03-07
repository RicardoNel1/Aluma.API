using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("consumer_protection")]
    public class ConsumerProtectionModel : BaseModel
    {
        public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool InformProducts { get; set; }
        public bool InformOffers { get; set; }
        public bool RequestResearch { get; set; }
        public CommEnum PreferredComm { get; set; }
        public bool OtherCommMethods { get; set; }

    }

    public class ConsumerProtectionModelBuilder : IEntityTypeConfiguration<ConsumerProtectionModel>
    {
        public void Configure(EntityTypeBuilder<ConsumerProtectionModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
           
        }
    }

    

    
}