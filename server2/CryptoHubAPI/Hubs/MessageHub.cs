using BusinessLogic.Services.MessageService;
using BusinessLogic.Services.NotificationService;
using BusinessLogic.Services.UserService;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CryptoHubAPI.Hubs
{
    public class MessageHub : Hub
    {
        private static List<ChatUser> _users = new List<ChatUser>();
        private static List<Message> _messages = new List<Message>();

        private readonly IMessageService _messageService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public MessageHub(IMessageService messageService, INotificationService notificationService, IUserService userService)
        {
            _messageService = messageService;
            _notificationService = notificationService;
            _userService = userService;
        }

        /*public MessageHub()
        {
            Console.WriteLine("hey");
        }*/

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

        public override async Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            var user = _users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                var messages = _messages.FindAll(m => m.UserId == user.UserId);

                await _messageService.AddBatchMessages(messages);

                _messages.RemoveAll(m => m.UserId == user.UserId);

                _users.Remove(user);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {

            //await Clients.All.SendAsync("RecievedMessage", "a message was sent");
            Console.WriteLine("Message Recived on: " + Context.ConnectionId);

            var msg = JsonConvert.DeserializeObject<Message>(message);

            if (msg.UserId == null)
                return;


            msg.TimeDelivered = DateTime.UtcNow;
            
            var reciever = _users.FirstOrDefault(x => x.UserId == msg.RecieverId);

            var sendMessage = JsonConvert.SerializeObject(
                msg, 
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            if (reciever == null)
            {
                //var x = await _messageService.GetMessages(1, 2);
                
                _ = Clients.Caller.SendAsync("RecievedMessage", sendMessage);
                await _messageService.AddMessage(msg);
            }
            else
            {
                Console.WriteLine("reciver not null");
                _ = Clients.Caller.SendAsync("RecievedMessage", sendMessage);
                _ = Clients.Client(reciever.ConnectionId).SendAsync("RecievedMessage", sendMessage);
                _messages.Add(msg);
            }

            //_ = AddNotification(msg.UserId, msg.RecieverId);


        }

        public async Task AddNotification(int senderId, int reciverId)
        {
            var user = await _userService.GetById(reciverId);

            if (user == null)
                return;

            var notification = await _notificationService.GetNotification(reciverId, senderId);

            if (notification == null || notification.IsDeleted)
            {
                _ = _notificationService.AddNotification(new Notification
                {

                    UserId = reciverId,
                    SenderId = senderId,
                });

                var chatUser = _users.FirstOrDefault(x => x.UserId == user.UserId );
                if (chatUser != null)
                    _ = Clients.Clients(chatUser.ConnectionId).SendAsync("AddNotification");
            }
            else
                _ = _notificationService.AddNotification(notification);

        }

        public async Task RemoveNotification(int userId, int senderId)
        {
           

            var notification = await _notificationService.GetNotification(userId, senderId);

            if (notification != null && notification.IsDeleted == false)
            {
                await _notificationService.RemoveNotification(notification.UserId, notification.SenderId);

               
                await Clients.Caller.SendAsync("RemoveNotification");
            }
        }

        public async Task MarkAsRead(int userId, int senderId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == senderId);
            if (user != null)
                await Clients.Client(user.ConnectionId).SendAsync("Read");

            await RemoveNotification(userId, senderId);
        }
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
