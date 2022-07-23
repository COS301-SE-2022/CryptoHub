using BusinessLogic.Services.ImageService;
using System;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.ImageDTOs;
using AutoMapper;

namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _imageService.GetById(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ImageDTO>> AddImage(CreateImageDTO imageDTO)
        {
            var response = await _imageService.AddImage(imageDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            var image = _mapper.Map<ImageDTO>(response.Model);
            return Ok(image);

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _imageService.Delete(id);
            return Ok();
        }


    }
}

