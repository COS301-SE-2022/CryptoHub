using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TestEmailComtroller : Controller
    {
        [Route("index")]
        [HttpGet]
        public async Task<IActionResult> Index()
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
    }
}
