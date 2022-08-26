using BusinessLogic.Services.MessageService;
using BusinessLogic.Services.NotificationService;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        public async Task<List<Notification>> GetNotifications(int userId)
        {
            return await _notificationService.GetNotifications(userId);
        }
    }
}
