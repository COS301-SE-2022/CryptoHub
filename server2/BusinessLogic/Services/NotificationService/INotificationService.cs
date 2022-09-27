using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.NotificationService
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotifications(int userId);


        Task<Notification> GetNotification(int userId, int senderId);


        Task AddNotification(Notification notificsaion);


        Task RemoveNotification(int userId, int senderId);
       
    }
}
