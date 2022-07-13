using BusinessLogic.Services.ImageService;
using System;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.ImageDTOs;


namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
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
        public async Task<ActionResult<Image>> AddImage(ImageDTO imageDTO)
        {
            var response = await _imageService.AddImage(imageDTO);
            if (response == null)
                return BadRequest();

            return Ok(response);

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _imageService.Delete(id);
            return Ok();
        }


    }
}

