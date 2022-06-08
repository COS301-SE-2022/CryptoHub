using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class UserFollowerControllerTest
    {
        private readonly Mock<IUserFollowerRepository> _userFollowerRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserFollowerControllerTest()
        {
            _userFollowerRepositoryMock = new Mock<IUserFollowerRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetAllUsers_ListOfUsers_ReturnsListOfUsers()
        {
            //arrange
            List<UserFollower> userFollowers = new List<UserFollower>
            {
                new UserFollower
                {
                    Id = 1,
                    UserId = 1,
                    FollowId = 1,
                    FollowDate = new DateTime(2000, 1, 25)
                },
                new UserFollower
                {
                    Id = 2,
                    UserId = 2,
                    FollowId = 2,
                    FollowDate = new DateTime(2000, 1, 25)
                },
            };

            _userFollowerRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(userFollowers);

            var controller = new UserFollowerController(_userFollowerRepositoryMock.Object, _userRepositoryMock.Object);

            //act
            var result = await controller.GetAllUserFollowers();


            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);


            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<UserFollower>>(actual);
            Assert.Equal(2, (actual as List<UserFollower>).Count);


        }
    }
}
