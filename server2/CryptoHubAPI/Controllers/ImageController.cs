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

	}
}

