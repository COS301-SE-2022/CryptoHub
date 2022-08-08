using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CryptoHubAPI.Hubs
{
    public class MessageHub : Hub
    {
        private static List<User> _users = new List<User>();
        private static List<Message> _messages = new List<Message> 
        {
            new Message
            {
                From = "1",
                To = "2",
                Content = "hello"
            },
            new Message
            {
                From = "2",
                To = "1",
                Content = "hey"
            },

        }; 

        public override Task OnConnectedAsync()
        {
            var username = Context.GetHttpContext().Request.Query["username"].FirstOrDefault();
            var id = Context.GetHttpContext().Request.Query["userId"].FirstOrDefault();

            _users.Add(new User
            {
                Id = id,
                Name = username,
                ConnectionId = Context.ConnectionId

            });
            
            Console.WriteLine(Context.ConnectionId + " is Connected");

            var messages = JsonConvert.SerializeObject(_messages);

            Clients.Client(Context.ConnectionId).SendAsync("RecievedID", Context.ConnectionId,id, username,messages);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var user = _users.FirstOrDefault(x => x.ConnectionId ==  Context.ConnectionId);

            _users.Remove(user);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string message)
        {
            Console.WriteLine("Message Recived on: " + Context.ConnectionId);

            var msg = JsonConvert.DeserializeObject<Message>(message);

            //var reciever = _users.FirstOrDefault(x => x.Id == msg.To);
            var reciever = msg.To;

            //await Clients.All.SendAsync("RecievedMessage", msg);

            if (reciever == null)
            {
                _messages.Add(msg);
                await Clients.Caller.SendAsync("RecievedMessage", msg);
            }
            else
            {
                _messages.Add(msg);
                //await Clients.Client(reciever.ConnectionId).SendAsync("RecivedMessage", msg);
                await Clients.Client(reciever).SendAsync("RecievedMessage", message);
            }
        }   
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
    }

    public class Message
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Content  { get; set; }

        
    }
}
