using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.MessageService
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessages(int senderid, int recieverid);



        Task AddMessage(Message message);



        Task AddBatchMessages(List<Message> messages);
        
    }
}
