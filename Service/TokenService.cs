using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Animal2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Animal2.Service
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _Key;
        private readonly UserManager<Customer> _userManager;
        public TokenService(IConfiguration configuration, UserManager<Customer> _userManager)
        {
            _config = configuration;
            _Key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        public string CreateToken(Customer customer) {
            var claims = new List<Claim> {
            new  Claim(JwtRegisteredClaimNames.Sub,customer.Id),
            new  Claim(JwtRegisteredClaimNames.GivenName,customer.UserName),
            new  Claim(JwtRegisteredClaimNames.Email,customer.Email),
            new  Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var creds = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var   token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
       
    }
}
