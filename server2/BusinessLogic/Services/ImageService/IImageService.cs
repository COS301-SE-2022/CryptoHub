using Domain.Models;
using Infrastructure.DTO.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.ImageService
{
    public interface IImageService
    {
        Task<Image> GetById(int id);
        Task<Response<Image>> AddImage(CreateImageDTO imageDTO);
        Task Delete(int id);
    }
}
