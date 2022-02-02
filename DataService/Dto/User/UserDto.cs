using System;
using DataService.Enum;

namespace DataService.Dto
{
    public class UserDto : UserBaseDto
    {
        public AddressDto Address { get; set; }               
        
        public string DateOfBirth { get; set; }
        public RoleEnum Role { get; set; }
        public string ProfilePicture { get; set; }
        //public string Password { get; set; }

    }

}


//match UserModel (angular)