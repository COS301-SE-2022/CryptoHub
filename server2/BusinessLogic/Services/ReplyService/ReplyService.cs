using System;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.ReplyDTOs;

namespace BusinessLogic.Services.ReplyService
{
    public class ReplyService : IReplyService
    {
        private readonly IReplyRepository _replyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReplyService(IReplyRepository replyRepository,IUserRepository userRepository, IMapper mapper)
        {
            _replyRepository = replyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<ReplyDTO>> GetRepliesByUserId(int userId)
        {
            var user = await _userRepository.FindOne(u => u.UserId == userId);
            if (user == null)
                return null;

            var reply = await _replyRepository.FindRange(r => r.UserId == userId);
                return _mapper.Map<List<ReplyDTO>>(reply);
        }

        public Task<ReplyDTO> AddReply(Reply reply)
        {
            throw new NotImplementedException();
        }

        public Task<ReplyDTO> Delete(int replyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReplyDTO>> GetRepliesByCommentId(int commentId)
        {
            throw new NotImplementedException();
        }

        

        public Task<ReplyDTO> GetRepliesCountByCommentId(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<ReplyDTO> UpdateReply(int replyId, Reply reply)
        {
            throw new NotImplementedException();
        }
    }
}

