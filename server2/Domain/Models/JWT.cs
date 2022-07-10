using System;
namespace Domain.Models
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

