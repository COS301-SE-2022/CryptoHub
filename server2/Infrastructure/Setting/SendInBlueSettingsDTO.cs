using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Setting
{
    public class SendInBlueSettingsDTO
    {
        [JsonPropertyName("SENDINBLUE_NAME")]
        public string Name { get; set; }

        [JsonPropertyName("SENDINBLUE_EMAIL")]
        public string Email { get; set; }

        [JsonPropertyName("SENDINBLUE_KEY")]
        public string Key { get; set; }

        

        
    }
}
