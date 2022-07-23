using Newtonsoft.Json;

namespace Infrastructure.Setting
{
    public class JWTSettings
    {
        
        public static string Key { get; set; }

        
        public static string Issuer { get; set; }

        
        public static string Subject { get; set; }

        
        public static string Audience { get; set; }

        public static void Set(JWTSettingsDTO jWTSettingsDTO)
        {
            Key = jWTSettingsDTO.Key;
            Issuer = jWTSettingsDTO.Issuer;
            Subject = jWTSettingsDTO.Subject;
            Audience = jWTSettingsDTO.Audience;

        }
    }
}
