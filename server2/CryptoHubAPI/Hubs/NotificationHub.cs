using Microsoft.AspNetCore.SignalR;

namespace CryptoHubAPI.Hubs
{
    public class NotificationHub : BaseHub
    {
        public List<Notifications> _notifications { get; set; }

        public async Task AddNotification(int senderId, int reciverId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == reciverId);
            if (user == null)
                return;

            var notification = _notifications.FirstOrDefault(x => 
            x.userId == reciverId 
            && x.senderId == senderId);

            if (notification == null)
            {
                _notifications.Add(new Notifications
                {
                    userId = user.UserId,
                    senderId = senderId,
                });
            }
        }

        public async Task RemoveNotification(int senderId, int reciverId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == reciverId);
            if (user == null)
                return;

            var notification = _notifications.FirstOrDefault(x =>
            x.userId == reciverId
            && x.senderId == senderId);

            if (notification != null)
            {
                _notifications.Remove(notification);
            }
        }

        public async Task MarkAsRead(int senderId, int reciverId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == reciverId);
            if (user == null)
                return;

            await Clients.Client(user.ConnectionId).SendAsync("Read");

            await RemoveNotification(senderId, reciverId);
        }


    }

    public class Notifications
    {
        public int userId { get; set; }

        public int senderId { get; set; }
    }
}
