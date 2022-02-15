using System.ComponentModel.DataAnnotations;
using DataService.Enum;
using System;

namespace DataService.Dto
{
    public class RegistrationDto
    {
        public UserDto User { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name max length is 60 characters")]
        [MinLength(2, ErrorMessage = "First name min length is 2 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name max length is 60 characters")]
        [MinLength(2, ErrorMessage = "Last name min length is 2 characters")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "ID number is required")]
        //[StringLength(13, ErrorMessage = "ID number max length is 13 characters")]
        //[MinLength(6, ErrorMessage = "ID number min length is 6 characters")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(60, ErrorMessage = "Email max length is 60 characters")]
        [MinLength(10, ErrorMessage = "Email min length is 10 characters")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(9, ErrorMessage = "Mobile number max length is 9 characters")]
        [MinLength(9, ErrorMessage = "Mobile number min length is 9 characters")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, ErrorMessage = "Password max length is 16 characters")]
        [MinLength(8, ErrorMessage = "Password min length is 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string SocialId { get; set; }
    }
}