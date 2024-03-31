using Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
                new Claim(ClaimTypes.Surname,user.LastName)
            };
             var credientials = new SigningCredentials(_jwtkey,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Userclaims),
                Expires = System.DateTime.Now.AddDays(int.Parse(configuration["Jwt:ExpiresInDays"])),
                SigningCredentials = credientials,
                Issuer = configuration["Jwt:Issuer"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }
    }
}
