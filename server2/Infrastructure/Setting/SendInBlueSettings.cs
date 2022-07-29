using Newtonsoft.Json;

namespace Infrastructure.Setting
{
    public class SendInBlueSettings
    {
        
        public static string Name { get; set; }

        
        public static string Email { get; set; }

        
        public static string Key { get; set; }

        public static void Set(SendInBlueSettingsDTO sendInBlueSettingsDTO)
        {
            Name = sendInBlueSettingsDTO.Name;
            Email = sendInBlueSettingsDTO.Email;
            Key = sendInBlueSettingsDTO.Key;
        }


    }
}
