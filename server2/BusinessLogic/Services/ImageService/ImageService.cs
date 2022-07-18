using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.Repository;
using Intergration.FireStoreService;
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
        private readonly IFireStorageService _fireStorageService;
        public ImageService(IImageRepository imageRepository, IMapper mapper, IFireStorageService fireStorageService)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _fireStorageService = fireStorageService;
        }

        public async Task<Image> GetById(int id)
        {
            var response = await _imageRepository.GetById(u => u.ImageId == id);

            return _mapper.Map<Image>(response);

        }

        public async Task<Response<Image>> AddImage(CreateImageDTO imageDTO)
        {
           var response =  await _fireStorageService.UploadImage(imageDTO);

            if (response.HasError)
                return response;

            var image = await _imageRepository.Add(response.Model);
            return new Response<Image>(image, false, "");



            
        }

        public async Task Delete(int id)
        {
            await _imageRepository.DeleteOne(u => u.ImageId == id);
        }
    }
}
