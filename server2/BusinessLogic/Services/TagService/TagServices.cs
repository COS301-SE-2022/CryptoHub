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

        public async Task<List<Tag>> GetTags()
        {
            return await _tagRepository.ListByExpression(t => true);
        }

        public async Task<Tag> GetTagbyLabel(string tagLabel)
        {
            var tag = await _tagRepository.GetByExpression(t => t.Content == tagLabel);
            if (tag == null)
                return null;

            return tag;
        }


        public async Task<Response<string>> AddTag(string tagLabel)
        {
            if (!tagLabel.StartsWith('#'))
                return new Response<string>(null, false, "tag must start with #");

            var tag = _tagRepository.GetByExpression(t => t.Content == tagLabel);

            if (tag != null)
                return new Response<string>(null, false, "tag already exits");

            await _tagRepository.Add(new Tag { Content = tagLabel});
            
            return new Response<string>(null, false, "tag added");
        }

        
    }
}
