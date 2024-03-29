using Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services
{
    public class JwtService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey _jwtkey;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
            _jwtkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        }
        public string GenerateJWT(Users user)
        {
            var Userclaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),

            }
        }
    }
}
