using Domain.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> GetNotifications(int userId)
        {
            return await _notificationRepository.ListByExpression(n => n.UserId == userId);
        }

        public async Task<Notification> GetNotification(int userId, int senderId)
        {
            var notification = await _notificationRepository.GetByExpression(n =>
           n.UserId == userId
           && n.SenderId == senderId
           );

            return notification ?? null;
        }

        public async Task AddNotification(Notification notificsaion)
        {
            var response = await _notificationRepository.GetByExpression(n => 
            n.UserId == notificsaion.UserId
            && n.SenderId == notificsaion.SenderId
            );

            if (response == null)
            {
                response = new Notification()
                response.UserId = notificsaion.UserId;
                response.SenderId = notificsaion.SenderId;
            }

            response.IsDeleted = false;
            response.LastModified = DateTime.UtcNow;

            await _notificationRepository.Update(response);
        }

        public async Task RemoveNotification(int userId, int senderId)
        {
            var notification = await _notificationRepository.GetByExpression(n =>
            n.UserId == userId
            && n.SenderId == senderId
            );

            if(notification == null)
                return;

            notification.IsDeleted = true;
            notification.LastModified = DateTime.UtcNow;

            await _notificationRepository.Update(notification);

        }
    }
}
