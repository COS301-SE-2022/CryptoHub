using System;
using System.Security.Claims;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace CryptoHubAPI.Authentication
{
    public class GenerateToken 
    {
        private readonly IConfiguration _configuration;



        public GenerateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.PrimarySid, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Firstname),
                new Claim(ClaimTypes.Surname, user.Lastname),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            return string.Empty;
        }
    }
}

