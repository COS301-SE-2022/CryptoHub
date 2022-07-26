using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.TagDTOs;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.TagService
{
    public class TagServices : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public TagServices(ITagRepository tagRepository, IPostRepository postRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _postRepository = postRepository;
            _mapper = mapper;

        }

        public async Task<ICollection<TagDTO>> GetTagsByPost(int id)
        {
            var tag = await _tagRepository.FindRange(t => t.PostId == id);
            return _mapper.Map<ICollection<TagDTO>>(tag);
        }

        public async Task<Response<string>> BatchAddTag(int postId, BatchTagsDTO batchTagsDTO)
        {
            var post = await _postRepository.GetByExpression(p => p.PostId == postId);

            if (post == null)
                return new Response<string>(null, true, "post not found");

            foreach (var item in batchTagsDTO.Tags)
            {
                if (item.StartsWith('#'))
                {

                    var tag = new Tag
                    {
                        PostId = postId,
                        Content = item
                    };
                    await _tagRepository.Add(tag);
                }
            };

            return new Response<string>(null, false, "tags added to post");
        }
    }
}
