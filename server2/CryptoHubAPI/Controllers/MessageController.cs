using BusinessLogic.Services.MessageService;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{senderId}/{reciverId}")]
        public async Task<List<Message>> GetMessages(int senderId, int reciverId)
        {
            return await _messageService.GetMessages(senderId, reciverId);
        }
    }
}
