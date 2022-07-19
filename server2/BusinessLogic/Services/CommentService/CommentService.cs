using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.CommentDTOs;

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

        public async Task<List<CommentDTO>> GetCommentByPostId(int id)
        {
            var response = await _commentRepository.ListByExpression(c => c.PostId == id);
            if (response == null)
                return null;

            return _mapper.Map<List<CommentDTO>>(response);
        }

        public async Task<Response<object>> GetCommentCountByPostId(int id)
        {
            var response = await _commentRepository.FindRange(c => c.PostId == id);
            if (response == null)
                return new Response<object>(null, true, "no comments");

            return new Response<object>(new { Count = response.Count() }, false, "");

        }

        public async Task<CommentDTO> AddComment(CommentDTO comment)
        {
            var newComment = new Comment
            {
                Content = comment.Content,
                UserId = comment.UserId,
                PostId = comment.PostId
            };

            await _commentRepository.Add(newComment);

            return _mapper.Map<CommentDTO>(newComment);
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
