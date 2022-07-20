//using Firebase.Auth;
//using Firebase.Storage;
//using Infrastructure.DTO.ImageDTOs;
//using Microsoft.AspNetCore.Mvc;
//using MimeKit;
//using MimeKit.Text;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using SixLabors.ImageSharp.Formats;
//using static System.Net.Mime.MediaTypeNames;
//using sib_api_v3_sdk.Api;
//using sib_api_v3_sdk.Client;
//using sib_api_v3_sdk.Model;

//namespace CryptoHubAPI.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]/[action]")]
//    public class TestController : Controller
//    {
//        [HttpGet]
//        public async Task<IActionResult> Email()
//        {
//            /*var apiKey = "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";
//            var client = new SendGridClient(apiKey);
//            var from = new EmailAddress("codeforceup@gmail.com", "CodeForce");
//            var subject = "Sending with SendGrid is Fun";
//            var to = new EmailAddress("borekhotso@gmail.com", "Khotso");
//            var plainTextContent = "and easy to do anywhere, even with C#";
//            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//            await client.SendEmailAsync(msg);
//            return Ok();*/

//            /*// create email message
//            var email = new MimeMessage();
//            email.From.Add(MailboxAddress.Parse("borekhotso@gmail.com"));
//            email.To.Add(MailboxAddress.Parse("codeforceup@gmail.com"));
//            email.Subject = "Test Email Subject";
//            email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

//            // send email
//            using var smtp = new SmtpClient();
//            //smtp.Connect("localhost",7215,false);
//            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
//            smtp.Authenticate("borekhotso@gmail.com", "kb072jan");
//            smtp.Send(email);
//            smtp.Disconnect(true);*/

//            Configuration.Default.ApiKey.Add("api-key", "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");
//            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
//            // Configuration.Default.ApiKeyPrefix.Add("api-key", "Bearer");
//            // Configure API key authorization: partner-key
//            //Configuration.Default.ApiKey.Add("partner-key", "YOUR_PARTNER_KEY");
//            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
//            // Configuration.Default.ApiKeyPrefix.Add("partner-key", "Bearer");

//            /* var apiInstance = new AccountApi();


//            GetAccount result = apiInstance.GetAccount();*/

//            var apiInstance = new TransactionalEmailsApi();
//            var sendSmtpEmail = new SendSmtpEmail
//            {
//                Sender = new SendSmtpEmailSender("CodeForce","codeforceup@gmail.com"),
//                Subject = "TEST EMAIL",
//                To = new List<SendSmtpEmailTo>
//                {
//                    new SendSmtpEmailTo("borekhotso@gmail.com","Khotso")
//                },
//                TextContent = " a cool email",
//                //HtmlContent = "CryptoHUB Sauce<",
//            };

            
//                // Send a transactional email
//                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);


//                return Ok(result);

//        }

//        [HttpPost]
//        public async Task<IActionResult> Images(CreateImageDTO imageDTO)
//        {

//            /* byte[] bytes = Convert.FromBase64String(imageDTO.Blob) ;

//             var ms = new MemoryStream(bytes);

//             IImageFormat format = SixLabors.ImageSharp.Image.DetectFormat(bytes);

//             var type = format.Name;

//             //authentication
//             var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAs6bxiM71e6LE4E8-pGzUNL3OGeyE8iTA"));
//             var a = await auth.SignInWithEmailAndPasswordAsync("codeforceup@gmail.com", "Vibesinnit");

//             // Constructr FirebaseStorage, path to where you want to upload the file and Put it there
//             var task = new FirebaseStorage(
//                 "cryptohub-12abc.appspot.com",
//                  new FirebaseStorageOptions
//                  {
//                      AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
//                      ThrowOnCancel = true,
//                  })
//                 .Child(imageDTO.Name)
//                 .PutAsync(ms);

//             // await the task to wait until upload completes and get the download url
//             return Ok(await task);*/
//            return Ok();
//        }
//    }
//}
