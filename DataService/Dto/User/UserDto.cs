using DataService.Enum;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class UserDto : UserBaseDto
    {
        //public AddressDto Address { get; set; }            
        public List<AddressDto> Address { get; set; }
        public string DateOfBirth { get; set; }
        public RoleEnum Role { get; set; }
        public string ProfilePicture { get; set; }

        public bool HasSignature { get; set; }
    }

    

}

