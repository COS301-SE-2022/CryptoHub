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


        public async Task<List<Image>> GetAll()
        {
            return await _imageRepository.GetAll();

        }

        public async Task<Image> GetById(int id)
        {
            var response = await _imageRepository.GetById(u => u.ImageId == id);

            return _mapper.Map<Image>(response);

        }

        public async Task<Response<Image>> AddImage(CreateImageDTO imageDTO)
        {

            var image = await _imageRepository.GetByExpression(image => image.Name.Contains(imageDTO.Name));

            if (image != null)
            {
                await _fireStorageService.DeleteImage(image.Name);
            }
            else
            {
                image = new Image();
            }

            var response =  await _fireStorageService.UploadImage(imageDTO);

            if (response.HasError)
                return response;

            image.Url = response.Model.Url;
            image.Name = response.Model.Name;

            await _imageRepository.Update(image);
            return new Response<Image>(image, false, "");
        }

   

        public async Task Delete(int id)
        {
            await _imageRepository.DeleteOne(u => u.ImageId == id);
        }
    }
}
