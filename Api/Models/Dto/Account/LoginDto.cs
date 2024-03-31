using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto.Account
{
    public class LoginDto
    {
        [Required]

        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
