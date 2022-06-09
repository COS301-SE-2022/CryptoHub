using CryptoHubAPI;
using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class ImageControllerTest
    {
        private readonly Mock<IImageRepository> _imageRepositoryMock;

        public ImageControllerTest()
        {
            _imageRepositoryMock = new Mock<IImageRepository>();
        }

        [Fact]
        public async Task GetById_Id_ReturnsImageOfId()
        {
            //arrange
            var image = new Image
            {
                ImageId = 1,
            };

            _imageRepositoryMock.Setup(u => u.GetById(It.IsAny<Expression<Func<Image, bool>>>())).ReturnsAsync(image);
            //_postRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(posts);

            var controller = new ImageController(_imageRepositoryMock.Object);


            //act
            var result = await controller.GetById(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var actual = (result as OkObjectResult).Value;
            Assert.IsType<Image>(actual);
        }

        [Fact]
        public async Task AddImage_Image_ReturnsImage()
        {
            //arrange
            var image = new Image
            {
                ImageId = 1,
            };
            var imageODT = new imageDTO
            {
                Blob = "100110 111010 001011 101001"
            };
            _imageRepositoryMock.Setup(u => u.Add(It.IsAny<Image>())).ReturnsAsync(image);

            var controller = new ImageController(_imageRepositoryMock.Object);

            //act
            var result = await controller.AddImage(imageODT);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Image>(actual);
        }
    }
}
