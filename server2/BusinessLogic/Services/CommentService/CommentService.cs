using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.CommentDTOs;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentDTO> GetCommentByUserId(int id)
        {
            var response = await _commentRepository.GetById(c => c.UserId == id);
            if (response == null)
                return null;

            return _mapper.Map<CommentDTO>(response);
        }

        public async Task<CommentDTO> GetCommentByPostId(int id)
        {
            var response = await _commentRepository.GetById(c => c.PostId == id);
            if (response == null)
                return null;

            return _mapper.Map<CommentDTO>(response);
        }

        public async Task<Response<object>> GetCommentCountByPostId(int id)
        {
            var response = await _commentRepository.FindRange(c => c.PostId == id);
            if (response == null)
                return new Response<object>(null, true, "no comments");

            return new Response<object>(new { Count = response.Count() }, false, "");

        }

        public async Task<CommentDTO> AddComment(Comment comment)
        {
            await _commentRepository.Add(comment);

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<CommentDTO> UpdateComment(Comment comment)
        {
            var response = await _commentRepository.Update(c => c.CommentId == comment.CommentId, comment);
            if (response == null)
                return null;

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task Delete(int id)
        {
            await _commentRepository.DeleteOne(c => c.CommentId == id);
        }
    }
}
