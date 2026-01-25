using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace StudentManagement.Application.Util.jwt
{
    public interface IGenerateJwtToken
    {
        string GenerateJwtToken(List<Claim> claims);
    }
    public class GenerateJwtTokenService : IGenerateJwtToken
    {
        private readonly IConfiguration _config;
        public GenerateJwtTokenService(IConfiguration config) => _config = config;
        public string GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                issuer: "StudentApi",
                audience: "StudentApiUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}