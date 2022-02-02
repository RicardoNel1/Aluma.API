using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("user_documents")]
    public class UserDocumentModel : DocumentBaseModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}