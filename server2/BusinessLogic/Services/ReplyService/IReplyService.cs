using System;
using Domain.Models;
using Infrastructure.DTO.ReplyDTOs;

namespace BusinessLogic.Services.ReplyService
{
    public interface IReplyService
    {
      Task<List<ReplyDTO>> GetRepliesByUserId(int userId);
      Task<List<ReplyDTO>> GetRepliesByCommentId(int commentId);
      Task<ReplyDTO> GetRepliesCountByCommentId(int commentId);
      Task<ReplyDTO> AddReply(Reply reply);
      Task<ReplyDTO> UpdateReply(int replyId, Reply reply);
      Task Delete(int replyId);


    }
}

