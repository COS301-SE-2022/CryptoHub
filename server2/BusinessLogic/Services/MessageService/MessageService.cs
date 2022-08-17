using Domain.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.MessageService
{
    public class MessageService : IMessageService
    {

        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Message>> GetMessages(int senderid, int recieverid)
        {
       
            var messages = await _messageRepository.ListByExpression(m =>
            (m.UserId == senderid || m.RecieverId == senderid)
            && (m.UserId == recieverid || m.RecieverId == recieverid));

            return messages;
        }

        public async Task AddMessage(Message message)
        {
            await _messageRepository.Add(message);
        }


        public async Task AddBatchMessages(List<Message> messages)
        {
            await _messageRepository.AddRange(messages);
        }

        public async Task MarkAsRead(int id)
        {
            var message = await _messageRepository.GetByExpression(m => m.Id == id);

            if (message == null)
                return;

            message.Read = true;
            await _messageRepository.Update(message);
        }


    }
}
