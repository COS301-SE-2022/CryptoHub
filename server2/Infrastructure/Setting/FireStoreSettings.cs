using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Setting
{
    public class FireStoreSettings
    {
        
        public static string Email { get; set; }

        
        public static string Password { get; set; }

        
        public static string APIKey { get; set; }

        
        public static string StorageBucket { get; set; }

        public static void Set(FireStoreSettingsDTO fireStoreSettingsDTO)
        {
            Email = fireStoreSettingsDTO.Email;
            Password = fireStoreSettingsDTO.Password;
            APIKey = fireStoreSettingsDTO.APIKey;
            StorageBucket = fireStoreSettingsDTO.StorageBucket;
        }
    }
}
