using System;
namespace CryptoHubAPI.Authentication
{
    public class JWT
    {
        public string Token { get; set; }
            
        public JWT(string token)
        {
            Token = token;
        }

        
    }
}

