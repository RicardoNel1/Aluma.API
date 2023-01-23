using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_notes")]
    public class ClientNotesModel : BaseModel
    {   
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string NoteBody { get; set; }
    }

    public class ClientNotesModelBuilder : IEntityTypeConfiguration<ClientNotesModel>
    {
        public void Configure(EntityTypeBuilder<ClientNotesModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }

}