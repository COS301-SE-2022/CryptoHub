/*using Microsoft.AspNetCore.SignalR;

namespace CryptoHubAPI.Hubs
{
    public class BaseHub : Hub
    {
        protected static List<ChatUser> _users = new List<ChatUser>();

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

            Clients.Client(Context.ConnectionId).SendAsync("RecievedID", Context.ConnectionId, id);
            return base.OnConnectedAsync();
        }

        public override async Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            var user = _users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            *//*var messages = _messages.FindAll(m => m.SenderId == user.UserId);

            await _messageService.AddBatchMessages(messages);

            _messages.RemoveAll(m => m.SenderId == user.UserId);*//*

            _users.Remove(user);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
*/