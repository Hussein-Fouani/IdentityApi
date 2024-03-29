using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Users:IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        public string LastName { get; set; }
        public DateTime DateCreated{ get; set; } = DateTime.Now;
    }
}
