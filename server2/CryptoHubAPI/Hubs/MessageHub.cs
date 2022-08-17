using BusinessLogic.Services.MessageService;
using BusinessLogic.Services.NotificationService;
using BusinessLogic.Services.UserService;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CryptoHubAPI.Hubs
{
    public class MessageHub : Hub
    {
        private static List<ChatUser> _users = new List<ChatUser>();
        private static List<Message> _messages = new List<Message>();

        /*private readonly IMessageService _messageService;
        private readonly INotificationService _notificationService;
*/
      /*  public MessageHub(IMessageService messageService, INotificationService notificationService)
        {
            _messageService = messageService;
            _notificationService = notificationService;
        }*/

        public MessageHub()
        {
            Console.WriteLine("hey");
        }

        public override Task OnConnectedAsync()
        {
            //var x = 
            var id = Context.GetHttpContext().Request.Query["userId"].FirstOrDefault();

            var user = _users.FirstOrDefault(x => x.UserId.ToString() == id);
            if (user == null)
            {
                _users.Add(new ChatUser
                {
                    UserId = Int32.Parse(id),
                    ConnectionId = Context.ConnectionId
                });
            }
            else
            {
                user.ConnectionId = Context.ConnectionId;
            }

            Console.WriteLine(Context.ConnectionId + " is Connected");

            Clients.Client(Context.ConnectionId).SendAsync("RecievedID", Context.ConnectionId,id);
            return base.OnConnectedAsync();
        }

        /*public override async Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            var user = _users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            var messages = _messages.FindAll(m => m.SenderId == user.UserId);

            await _messageService.AddBatchMessages(messages);

            _messages.RemoveAll(m => m.SenderId == user.UserId);

            _users.Remove(user);

            return base.OnDisconnectedAsync(exception);
        }*/

        /*public async Task SendMessageAsync(string message)
        {
            Console.WriteLine("Message Recived on: " + Context.ConnectionId);

            var msg = JsonConvert.DeserializeObject<Message>(message);

            var reciever = _users.FirstOrDefault(x => x.UserId == msg.RecieverId);
          
            if (reciever == null)
            {
                await _messageService.AddMessage(msg);
                await Clients.Caller.SendAsync("RecievedMessage", msg);
            }
            else
            {
                _messages.Add(msg);
                await Clients.Caller.SendAsync("RecievedMessage", msg);
                await Clients.Client(reciever.ConnectionId).SendAsync("RecievedMessage", message);
            }

            await AddNotification(msg.SenderId, msg.RecieverId);


        }

        public async Task AddNotification(int senderId, int reciverId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == reciverId);

            if (user == null)
                return;
            
            var notification = await _notificationService.GetNotification(reciverId,senderId);

            if (notification==null || notification.IsDeleted)
            {
                await _notificationService.AddNotification(new Notification
                {

                    UserId = reciverId,
                    SenderId = senderId,
                });

                await Clients.Clients(user.ConnectionId).SendAsync("AddNotification");
            }
            else
                await _notificationService.AddNotification(notification);

        }

        public async Task RemoveNotification(int senderId, int reciverId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == reciverId);
            if (user == null)
                return;

            var notification = await _notificationService.GetNotification(reciverId, senderId);

            if (notification != null)
            {
                _notificationService.RemoveNotification(notification.UserId,notification.SenderId);

                await Clients.Clients(user.ConnectionId).SendAsync("RemoveNotification");
            }
        }

        public async Task MarkAsRead(int senderId, int reciverId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == reciverId);
            if (user == null)
                return;

            await Clients.Client(user.ConnectionId).SendAsync("Read");

            await RemoveNotification(senderId, reciverId);
        }*/
    }

    public class ChatUser
    {
        public int UserId { get; set; }

        public string ConnectionId { get; set; }
    }

    public class Notifications
    {
        public int userId { get; set; }

        public int senderId { get; set; }
    }
}
