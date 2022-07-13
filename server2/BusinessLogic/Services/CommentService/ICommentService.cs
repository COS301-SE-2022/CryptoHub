using Domain.Models;
using Infrastructure.DTO.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CommentService
{
    public interface ICommentService
    {
        Task<CommentDTO> GetCommentByUserId(int id);
        Task<CommentDTO> GetCommentByPostId(int id);
        Task<Response<object>> GetCommentCountByPostId(int id);
        Task<CommentDTO> AddComment(Comment comment);
        Task<CommentDTO> UpdateComment(Comment comment);
        Task Delete(int id);
    }
}
