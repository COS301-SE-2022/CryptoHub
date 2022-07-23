using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Setting
{
    public class JWTSettingsDTO
    {
        [JsonPropertyName("JWT_KEY")]
        public string Key { get; set; }

        [JsonPropertyName("JWT_ISSUER")]
        public string Issuer { get; set; }

        [JsonPropertyName("JWT_SUBJECT")]
        public string Subject { get; set; }

        [JsonPropertyName("JWT_AUDIENCE")]
        public string Audience { get; set; }
    }
}
