using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("us_persons")]
    public class USPersonsModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IRSW8Id { get; set; }
        public IRSW8Model IRSW8 { get; set; }
        public string UsOwnerPassiveNFFEName { get; set; }
        public string UsOwnerPassiveNFFETin { get; set; }
        public string UsOwnerPassiveNFFEAddress1 { get; set; }
    }
}