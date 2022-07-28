using System.Text;
using System.Text.Json;

namespace Infrastructure.Setting
{
    public class Doppler
    {
        private static HttpClient client = new HttpClient();

        public static async Task FetchSecretsAsync()
        {
            var dopplerToken = Environment.GetEnvironmentVariable("DOPPLER_TOKEN");
            Console.WriteLine($"THE TOEKN IS {dopplerToken}");
            var basicAuthHeaderValue = Convert.ToBase64String(Encoding.Default.GetBytes(dopplerToken + ":"));

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", basicAuthHeaderValue);
            //var stream = await client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json");
            var fireStoreStream = await client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json"); ;
            var jwtStream = await client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json"); ;
            var sendInBlueStream = await client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json"); ;
            var dbConnectionStream = await client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json"); ;
            //var streamTask = await client.GetAsy("https://api.doppler.com/v3/configs/config/secrets/download?format=json");


            var firStoreSettings = await JsonSerializer.DeserializeAsync<FireStoreSettingsDTO>(fireStoreStream);
            var jwtSettings = await JsonSerializer.DeserializeAsync<JWTSettingsDTO>(jwtStream);
            var sendInBlueSettings = await JsonSerializer.DeserializeAsync<SendInBlueSettingsDTO>(sendInBlueStream);
            var dbConnectionSettings = await JsonSerializer.DeserializeAsync<DBConnctionSettingsDTO>(dbConnectionStream);


            FireStoreSettings.Set(firStoreSettings);
            JWTSettings.Set(jwtSettings);
            SendInBlueSettings.Set(sendInBlueSettings);
            DBConnctionSettings.Set(dbConnectionSettings);
        }
    }
}
