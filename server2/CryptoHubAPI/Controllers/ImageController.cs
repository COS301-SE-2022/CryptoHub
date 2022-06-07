﻿using System;
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

	}
}

