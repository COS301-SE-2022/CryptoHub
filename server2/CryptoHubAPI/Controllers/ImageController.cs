using System;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _imageRepository.GetById(u => u.ImageId == id);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<Image>> AddImage(imageDTO imageDTO)
        {
            byte[] imageArray = Convert.FromBase64String(imageDTO.Blob);

            Image image = new Image();
            image.Image1 = imageArray;
            
            return Ok(await _imageRepository.Add(image));

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _imageRepository.DeleteOne(u => u.ImageId == id);
            return Ok();
        }


    }
}

