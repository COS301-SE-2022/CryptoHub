using Firebase.Auth;
using Firebase.Storage;
using Infrastructure.DTO.ImageDTOs;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SixLabors.ImageSharp.Formats;
using static System.Net.Mime.MediaTypeNames;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Email()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("codeforceup@gmail.com", "CodeForce");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("u19180642@tuks.co.za", "Khotso");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Images(ImageDTO imageDTO)
        {

            byte[] bytes = Convert.FromBase64String(imageDTO.Blob) ;

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
            return Ok(await task);
        }
    }
}
