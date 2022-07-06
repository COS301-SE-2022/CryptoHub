using System;
using System.Security.Claims;
using Domain.Models;

namespace CryptoHubAPI.Authentication
{
    public class GenerateToken
    {
        public GenerateToken()
        {
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
            return string.Empty;
        }
    }
}

