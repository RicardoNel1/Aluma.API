using DataService.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    public class DocumentBaseModel : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public byte[] DocumentData { get; set; }
        public string URL { get; set; }

        public FileTypesEnum FileType { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DocumentTypesEnum DocumentType { get; set; }
    }
}