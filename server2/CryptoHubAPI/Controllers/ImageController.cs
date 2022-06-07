using System;
using Domain.IRepository;
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
		public async Task<ActionResult<Image>> GetUserById(int id)
		{
			var response = await _imageRepository.GetById(u => u.UserId == id);
			if (response == null)
				return NotFound();

			return Ok(response);

		}

		[HttpPost]
		public async Task<ActionResult<Image>> AddUser([FromBody] Image image)
		{
			return Ok(await _imageRepository.Add(image));

		}


		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _imageRepository.DeleteOne(u => u.UserId == id);
			return Ok();
		}


	}
}

