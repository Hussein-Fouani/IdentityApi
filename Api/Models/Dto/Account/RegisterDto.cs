using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto.Account
{
    public class RegisterDto
    {
        [Required]
        [PersonalData]
        [StringLength(15, MinimumLength = 3,ErrorMessage ="First Name Must be At least {2} and maximum {1}")]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Last Name Must be At least {2} and maximum {1}")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "UserName Must be At least {2} and maximum {1}")]
        public string Password { get; set; }
    }
}
