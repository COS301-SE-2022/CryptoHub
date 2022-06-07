using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class LikeControllerTest
    {
        private readonly Mock<ILikeRepository> _likeRepositoryMock;

        public LikeControllerTest()
        {
            _likeRepositoryMock = new Mock<ILikeRepository>();
        }

        [Fact]
        public async Task GetAllLikes_ListOfLikes_ReturnsListOfLikes()
        {
            //arrange
            List<Like> likes = new List<Like>
            {
                new Like
                {
                    LikeId = 1,
                    UserId = 1,
                    PostId = 1,
                },
                new Like
                {
                    LikeId = 2,
                    UserId = 2,
                    PostId = 2,
                },
                new Like
                {
                    LikeId = 3,
                    UserId = 3,
                    PostId = 3,
                }
            };

            _likeRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(likes);

            var controller = new LikeController(_likeRepositoryMock.Object);

            //act
            var result = await controller.GetAllLikes();


            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);


            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<Like>>(actual);
            Assert.Equal(3, (actual as List<Like>).Count);


        }
    }
}
