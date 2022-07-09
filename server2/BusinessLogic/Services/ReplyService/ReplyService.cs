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
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public ReplyService(IReplyRepository replyRepository, ICommentRepository commentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _replyRepository = replyRepository;
            _commentRepository = commentRepository;
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

        public async Task<List<ReplyDTO>> GetRepliesByCommentId(int commentId)
        {
            var comment = await _commentRepository.FindOne(u => u.CommentId == commentId);
            if (comment == null)
                return null;

            var reply = await _replyRepository.FindRange(r => r.CommentId == commentId);
            return _mapper.Map<List<ReplyDTO>>(reply);
        }

        public async Task<ReplyDTO> GetRepliesCountByCommentId(int commentId)
        {
            var comment = await _commentRepository.FindOne(u => u.CommentId == commentId);
            if (comment == null)
                return null;

            var reply = await _replyRepository.FindRange(r => r.CommentId == commentId);
            return _mapper.Map<ReplyDTO>(reply);
        }

        public async Task<ReplyDTO> AddReply(Reply reply)
        {
            var response = await _replyRepository.Add(reply);
            return _mapper.Map<ReplyDTO>(response);
        }
        public async Task<ReplyDTO> UpdateReply(int replyId, Reply reply)
        {
            var resposne = await _replyRepository.Update(r => r.ReplyId == replyId, reply);
            if (resposne == null)
                return null;

            return _mapper.Map<ReplyDTO>(resposne);
        }

        public async Task<ReplyDTO> Delete(int replyId)
        {
            await _replyRepository.DeleteOne(r => r.ReplyId == replyId);
            
        }

        

        

        

        
    }
}

