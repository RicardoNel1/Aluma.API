using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("application_documents")]
    public class ApplicationDocumentModel : DocumentBaseModel
    {
        public int ApplicationId { get; set; }
        public ApplicationModel Application { get; set; }
    }
}