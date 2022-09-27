//using BusinessLogic.Services.ImageService;
//using CryptoHubAPI.Controllers;
//using Domain.IRepository;
//using Domain.Models;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Linq.Expressions;
//using Infrastructure.DTO.ImageDTOs;
//using AutoMapper;

//namespace UnitTests.ControllerTests
//{
//    public class ImageControllerTest
//    {
//        private readonly Mock<IImageService> _imageServiceMock;
//        private readonly Mock<IMapper> _mapperMock;

//        public ImageControllerTest()
//        {
//            _imageServiceMock = new Mock<IImageService>();
//            _mapperMock = new Mock<IMapper>();
//        }

//        [Fact]
//        public async Task GetById_Id_ReturnsImageOfId()
//        {
//            //arrange
//            var image = new Image
//            {
//                ImageId = 1,
//            };

//            _imageServiceMock.Setup(u => u.GetById(1)).ReturnsAsync(image);
//            //_postRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(posts);

//            var controller = new ImageController(_imageServiceMock.Object, _mapperMock.Object);


//            //act
//            var result = await controller.GetById(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result);

//            var actual = (result as OkObjectResult).Value;
//            Assert.IsType<Image>(actual);
//        }

//        [Fact]
//        public async Task AddImage_Image_ReturnsImage()
//        {
//            //arrange
//            var image = new Image
//            {
//                ImageId = 1,
//            };
//            var imageODT = new CreateImageDTO
//            {
//                Name = "sample",
//                Blob = "100110 111010 001011 101001"
//            };
//            _imageServiceMock.Setup(u => u.AddImage(imageODT));

//            var controller = new ImageController(_imageServiceMock.Object, _mapperMock.Object);

//            //act
//            var result = await controller.AddImage(imageODT);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<Image>(actual);
//        }
//    }
//}
