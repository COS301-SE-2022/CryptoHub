using Firebase.Auth;
using Firebase.Storage;
using Infrastructure.DTO.ImageDTOs;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using SixLabors.ImageSharp.Formats;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Infrastructure.Setting;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        private static HttpClient client = new HttpClient();

        [HttpGet]

        public async Task<IActionResult> Keys()
        {
            /*var secrets = await Doppler.FetchSecretsAsync();

            *//*var keys = $"Project: {secrets.DopplerProject} \n " +
                $"Environment: {secrets.DopplerEnvironment} \n " +
                $"Config: {secrets.DopplerConfig}";
*/
            return Ok(FireStoreSettings.Email);
        }

        
    }

    /*public class Doppler
    {

        private static HttpClient client = new HttpClient();

        public static async Task FetchSecretsAsync()
        {
            var dopplerToken = Environment.GetEnvironmentVariable("DOPPLER_TOKEN");
            var basicAuthHeaderValue = Convert.ToBase64String(Encoding.Default.GetBytes(dopplerToken + ":"));

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", basicAuthHeaderValue);
            var streamTask = await client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json");
            //var streamTask = await client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets?project=cryptohub&config=dev&include_dynamic_secrets=false&dynamic_secrets_ttl_sec=1800");
            var fireStoreSettings = await JsonSerializer.DeserializeAsync<FireStoreSettings>(streamTask);
            var jwtSettings = await JsonSerializer.DeserializeAsync<JWTSettings>(streamTask);
            var sendInBlueSettings = await JsonSerializer.DeserializeAsync<SendInBlueSettings>(streamTask);
        }
    }*/
}
