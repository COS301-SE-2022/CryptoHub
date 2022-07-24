using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Setting
{
    public class FireStoreSettingsDTO
    {
        [JsonPropertyName("FIRESTORE_EMAIL")]
        public string Email { get; set; }

        [JsonPropertyName("FIRESTORE_PASSWORD")]
        public string Password { get; set; }

        [JsonPropertyName("FIRESTORE_APIKEY")]
        public string APIKey { get; set; }

        [JsonPropertyName("FIRESTORE_STORAGEBUCKET")]
        public string StorageBucket { get; set; }
    }
}
