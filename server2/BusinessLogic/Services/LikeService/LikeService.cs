using System;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.LikeDTOs;

namespace BusinessLogic.Services.LikeService
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;

        public LikeService(ILikeRepository likeRepository, IMapper mapper)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
        }

        public async Task<List<LikeDTO>> GetAllLikes() {
            var likes = await _likeRepository.GetAll();
            return _mapper.Map<List<LikeDTO>>(likes);
        }

        public async Task<List<LikeDTO>> GetLikeByUserId(int id) {

            var likes = await _likeRepository.FindRange(l => l.UserId == id);
            if (likes == null)
                return null;

            return _mapper.Map<List<LikeDTO>>(likes);


        }

        public async Task<List<LikeDTO>> GetLikeByPostId(int id)
        {
            var response = await _likeRepository.FindRange(l => l.PostId == id);
            if (response == null)
                return null;

            return _mapper.Map<List<LikeDTO>>(response);

        }

        public async Task<Response<object>> GetLikeCountByPostId(int id) {
            var response = await _likeRepository.FindRange(l => l.PostId == id);
            if (response == null)
                return new Response<object>( null, true, "No likes for this post");

            return new Response<object> (new { Count = response.Count() },false,string.Empty);
        }

        public async Task<List<LikeDTO>> GetLikeByCommentId(int id) {
            var response = await _likeRepository.FindRange(l => l.CommentId == id);
            if (response == null)
                return null;


            return _mapper.Map<List<LikeDTO>>(response);


        }

        public async Task<Response<object>> GetLikeCountByCommentId(int id) { 
            var response = await _likeRepository.FindRange(l => l.CommentId == id);
            if (response == null)
                 return new Response<object>(null, true, "No likes for this post");

            return new Response<object>(new { Count = response.Count() }, false, string.Empty);

    }

        public async Task<List<LikeDTO>> GetLikeByReplyId(int id) {
            var response = await _likeRepository.FindRange(l => l.ReplyId == id);
            if (response == null)
                return null;

            return _mapper.Map<List<LikeDTO>>(response);

        }

        public async Task<Response<object>> GetLikeCountByReplyId(int id) {

            var response = await _likeRepository.FindRange(l => l.ReplyId == id);
            if (response == null)
                return new Response<object>(null, true, "No likes for this post");

            return new Response<object>(new { Count = response.Count() }, false, string.Empty);
        }

        public async Task<LikeDTO> GetLikeBy(int userId, int postId) {
            var response = await _likeRepository.GetByExpression(p => p.UserId == userId && p.PostId == postId);
            if (response == null)
                return null;

            return _mapper.Map<LikeDTO>(response);

        }

        public async Task<LikeDTO> AddLike(Like like) {
            var likes = await _likeRepository.FindOne(l => l.UserId == like.UserId
              && l.ReplyId == like.ReplyId
              && l.CommentId == like.CommentId
              && l.PostId == like.PostId
              );

            if (likes != null)
                return null;

            return _mapper.Map<LikeDTO>(like);
        }

        public async Task<LikeDTO> UpdateLike(Like like) {
            var response = await _likeRepository.Update(u => u.LikeId == like.LikeId, like);
            if (response == null)
                return null;
            return _mapper.Map<LikeDTO>(response);
        }

        public async Task Delete(int userId, int postId) {

          await _likeRepository.DeleteOne(u => u.UserId == userId && u.PostId == postId);

            
        }
    }
}

