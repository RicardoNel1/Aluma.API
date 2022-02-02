using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class UserBaseDto
    {
        public ICollection<UserDocumentDto> Documents { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RSAIdNumber { get; set; }
        public string Email { get; set; }
        //public string Token { get; set; }
        public string MobileNumber { get; set; }
        public byte[] Signature { get; set; }
    }
}