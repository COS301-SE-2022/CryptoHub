using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<Image> GetById(int id)
        {
            var response = await _imageRepository.GetById(u => u.ImageId == id);

            return _mapper.Map<Image>(response);

        }

        public async Task<Image> AddImage(ImageDTO imageDTO)
        {
            byte[] imageArray = Convert.FromBase64String(imageDTO.Blob);

            Image image = new Image();
            image.Blob = imageArray;

            return _mapper.Map<Image>(await _imageRepository.Add(image));

        }

        public async Task Delete(int id)
        {
            await _imageRepository.DeleteOne(u => u.ImageId == id);
        }
    }
}
