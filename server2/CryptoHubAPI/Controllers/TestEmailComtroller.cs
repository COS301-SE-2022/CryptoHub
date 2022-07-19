using Firebase.Auth;
using Firebase.Storage;
using Infrastructure.DTO.ImageDTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using SixLabors.ImageSharp.Formats;
using static System.Net.Mime.MediaTypeNames;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Email()
        {
           

            Configuration.Default.ApiKey.Add("api-key", "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");
            

            var apiInstance = new TransactionalEmailsApi();
            var sendSmtpEmail = new SendSmtpEmail
            {
                Sender = new SendSmtpEmailSender("CodeForce","codeforceup@gmail.com"),
                Subject = "TEST EMAIL",
                To = new List<SendSmtpEmailTo>
                {
                    new SendSmtpEmailTo("borekhotso@gmail.com","Khotso")
                },
                TextContent = " a cool email",
                //HtmlContent = "CryptoHUB Sauce<",
            };

            
                // Send a transactional email
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);


            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Images(CreateImageDTO imageDTO)
        {

            /* byte[] bytes = Convert.FromBase64String(imageDTO.Blob) ;

             var ms = new MemoryStream(bytes);

             IImageFormat format = SixLabors.ImageSharp.Image.DetectFormat(bytes);

             var type = format.Name;

             //authentication
             var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAs6bxiM71e6LE4E8-pGzUNL3OGeyE8iTA"));
             var a = await auth.SignInWithEmailAndPasswordAsync("codeforceup@gmail.com", "Vibesinnit");

             // Constructr FirebaseStorage, path to where you want to upload the file and Put it there
             var task = new FirebaseStorage(
                 "cryptohub-12abc.appspot.com",
                  new FirebaseStorageOptions
                  {
                      AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                      ThrowOnCancel = true,
                  })
                 .Child(imageDTO.Name)
                 .PutAsync(ms);

             // await the task to wait until upload completes and get the download url
             return Ok(await task);*/
            return Ok();
        }
    }
}
