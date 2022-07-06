using System;
namespace CryptoHubAPI.Authentication
{
    public class JWT
    {
        private string Token { get; set; }
            
        public JWT(string token)
        {
            Token = token;
        }

        
    }
}

