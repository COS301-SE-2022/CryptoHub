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

        [Fact]
        public async Task GetUserUserFollower_Id_ReturnsNone()
        {
            //arrange
            List<UserFollower> userFollowers = new List<UserFollower>
            {
                new UserFollower
                {
                    Id = 1,
                    UserId = 1,
                    FollowId = 2,
                    FollowDate = new DateTime(2000, 1, 25)
                },
            };
            List<User> users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    Email = "johndoe@gmail.com",
                    Firstname = "john",
                    Lastname = "doe",
                    Username = "john",
                    Password = "1234"
                },
                new User
                {
                    UserId = 2,
                    Email = "elonmusk@gmail.com",
                    Firstname = "elon",
                    Lastname = "musk",
                    Username = "elon",
                    Password = "1234"
                },
                new User
                {
                    UserId = 3,
                    Email = "billgates@gmail.com",
                    Firstname = "bill",
                    Lastname = "gates",
                    Username = "bill",
                    Password = "windows"
                }
            };

            _userFollowerRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<UserFollower, bool>>>())).ReturnsAsync(userFollowers);
            _userRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(users);

            var controller = new UserFollowerController(_userFollowerRepositoryMock.Object, _userRepositoryMock.Object);

            //act
            var result = await controller.GetUserUserFollower(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            //var actual = (result as OkObjectResult).Value;


            //var x = actual.GetType().GetProperty("UserId").GetValue(actual, null);

            //Assert.Equal(2, x);
        }

        [Fact]
        public async Task FollowUser_UserIdAndTargetId_ReturnsUserFollowed()
        {
            //arrange
            UserFollower userFollower = new UserFollower
            {
                Id = 1,
                UserId = 1,
                FollowId = 1,
                FollowDate = new DateTime(2000, 1, 25)
            };

            _userFollowerRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<UserFollower, bool>>>())).ReturnsAsync(userFollower);


            var controller = new UserFollowerController(_userFollowerRepositoryMock.Object, _userRepositoryMock.Object);

            //act
            var result = await controller.FollowUser(1, 2);


            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var actual = (result as BadRequestObjectResult).Value;

            Assert.Equal("Already following this account", actual.ToString());

            //arrange 2
            _userFollowerRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<UserFollower, bool>>>())).ReturnsAsync((UserFollower)null);

            //act 2
            var result2 = await controller.FollowUser(1, 2);

            Assert.NotNull(result2);
            Assert.IsType<OkObjectResult>(result2);
            var actual2 = (result2 as OkObjectResult).Value;

            Assert.Equal("user followed", actual2.ToString());

        }
    }
}
