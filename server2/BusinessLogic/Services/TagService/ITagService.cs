using Domain.Models;
using Infrastructure.DTO.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.TagService
{
    public interface ITagService
    {
        Task<Tag> GetTagbyLabel(string tagLabel);

        Task<Response<string>> AddTag(string tagLabel);
    }
}
