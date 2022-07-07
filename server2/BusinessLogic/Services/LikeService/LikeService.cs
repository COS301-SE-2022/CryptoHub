using System;
using AutoMapper;
using Domain.IRepository;
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

        public async Task<LikeDTO> GetLikeCountByPostId(int id) {
        }

        public async Task<List<LikeDTO>> GetLikeByCommentId(int id) { }

        public async Task<LikeDTO> GetLikeCountByCommentId(int id) { }

        public async Task<List<LikeDTO>> GetLikeByReplyId(int id) { }

        public async Task<LikeDTO> GetLikeCountByReplyId(int id) { }

        public async Task<LikeDTO> GetLikeBy(int userId, int postId) { }

        public async Task<LikeDTO> AddLike(Like like) { }

        public async Task<LikeDTO> UpdateLike(Like like) { }

        public async Task<LikeDTO> Delete(int id) { }
    }
}

