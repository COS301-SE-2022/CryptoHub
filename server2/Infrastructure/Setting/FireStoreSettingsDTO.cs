using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Setting
{
    public class FireStoreSettingsDTO
    {
        [JsonPropertyName("FIRESTORE_EMAIL")]
        public string Email { get; set; }

        [JsonProperty("FIRESTORE_PASSWORD")]
        public string Password { get; set; }

        [JsonProperty("FIRESTORE_APIKEY")]
        public string APIKey { get; set; }

        [JsonProperty("FIRESTORE_STORAGEBUCKET")]
        public string StorageBucket { get; set; }
    }
}
