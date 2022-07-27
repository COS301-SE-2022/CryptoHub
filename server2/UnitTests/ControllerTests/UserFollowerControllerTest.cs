using CryptoHubAPI.Controllers;
using BusinessLogic.Services.UserFollowerService;
using BusinessLogic.Services.UserService;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using Infrastructure.DTO.UserFollowerDTOs;
using Infrastructure.DTO.UserDTOs;

namespace UnitTests.ControllerTests
{
    public class UserFollowerControllerTest
    {
        //private readonly Mock<IUserFollowerRepository> _userFollowerRepositoryMock;
        //private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserFollowerService> _userFollowerServiceMock;
        private readonly Mock<IUserService> _userServiceMock;

        public UserFollowerControllerTest()
        {
            //_userFollowerRepositoryMock = new Mock<IUserFollowerRepository>();
            //_userRepositoryMock = new Mock<IUserRepository>();
            _userFollowerServiceMock = new Mock<IUserFollowerService>();
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public async Task GetAllUsers_ListOfUsers_ReturnsListOfUsers()
        {
            //arrange
            List<UserFollowerDTO> userFollowers = new List<UserFollowerDTO>
            {
                new UserFollowerDTO
                {
                    Id = 1,
                    UserId = 1,
                    FollowId = 1,
                    FollowDate = new DateTime(2000, 1, 25)
                },
                new UserFollowerDTO
                {
                    Id = 2,
                    UserId = 2,
                    FollowId = 2,
                    FollowDate = new DateTime(2000, 1, 25)
                },
            };

            _userFollowerServiceMock.Setup(u => u.GetAllUserFollowers()).Returns(Task.FromResult(userFollowers));

            var controller = new UserFollowerController(_userFollowerServiceMock.Object);

            //act
            var result = await controller.GetAllUserFollowers();


            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);


            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<UserFollower>>(actual);
            Assert.Equal(2, (actual as List<UserFollower>).Count);
        }

        [Fact]
        public async Task GetUserUserFollower_Id_ReturnsUserIDAndUserName()
        {
            //arrange
            var userFollowers = new List<UserFollowerDTO>
            {
                new UserFollowerDTO
                {
                    Id = 1,
                    UserId = 1,
                    FollowId = 2,
                    FollowDate = new DateTime(2000, 1, 25)
                },
            };
            List<UserDTO> users = new List<UserDTO>
            {
                new UserDTO
                {
                    UserId = 1,
                    //Email = "johndoe@gmail.com",
                    Firstname = "john",
                    Lastname = "doe",
                    Username = "john"
                },
                new UserDTO
                {
                    UserId = 2,
                    //Email = "elonmusk@gmail.com",
                    Firstname = "elon",
                    Lastname = "musk",
                    Username = "elon"
                },
                new UserDTO
                {
                    UserId = 3,
                    //Email = "billgates@gmail.com",
                    Firstname = "bill",
                    Lastname = "gates",
                    Username = "bill"
                }
            };

            _userFollowerServiceMock.Setup(u => u.GetUserUserFollower(1)).Returns(Task.FromResult(userFollowers));
            _userServiceMock.Setup(u => u.GetAllUsers()).Returns(Task.FromResult(users));

            var controller = new UserFollowerController(_userFollowerServiceMock.Object);

            //act
            var result = await controller.GetUserFollower(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);



            var actual = (result as OkObjectResult).Value;
            var x = (actual as IEnumerable<object>).First();


            var userid = x.GetType().GetProperty("UserId").GetValue(x, null);
            Assert.Equal(2, userid);
        }

        [Fact]
        public async Task GetUserFollowing_Id_ReturnsUserIDAndUserName()
        {
            //arrange
            List<UserFollowerDTO> userFollowers = new List<UserFollowerDTO>
            {
                new UserFollowerDTO
                {
                    Id = 1,
                    UserId = 1,
                    FollowId = 2,
                    FollowDate = new DateTime(2000, 1, 25)
                },
            };
            List<UserDTO> users = new List<UserDTO>
            {
                new UserDTO
                {
                    UserId = 1,
                    //Email = "johndoe@gmail.com",
                    Firstname = "john",
                    Lastname = "doe",
                    Username = "john"
                },
                new UserDTO
                {
                    UserId = 2,
                    //Email = "elonmusk@gmail.com",
                    Firstname = "elon",
                    Lastname = "musk",
                    Username = "elon"
                },
                new UserDTO
                {
                    UserId = 3,
                    //Email = "billgates@gmail.com",
                    Firstname = "bill",
                    Lastname = "gates",
                    Username = "bill"
                }
            };

            _userFollowerServiceMock.Setup(u => u.GetUserFollowing(2)).Returns(Task.FromResult(userFollowers));
            _userServiceMock.Setup(u => u.GetAllUsers()).Returns(Task.FromResult(users));

            var controller = new UserFollowerController(_userFollowerServiceMock.Object);

            //act
            var result = await controller.GetUserFollowing(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);



            var actual = (result as OkObjectResult).Value;
            var x = (actual as IEnumerable<object>).First();


            var userid = x.GetType().GetProperty("UserId").GetValue(x, null);
            Assert.Equal(1, userid);
        }

        [Fact]
        public async Task FollowUser_UserIdAndTargetId_ReturnsUserFollowed()
        {
            //arrange
            UserFollowerDTO userFollower = new UserFollowerDTO
            {
                Id = 1,
                UserId = 1,
                FollowId = 2,
                FollowDate = new DateTime(2000, 1, 25)
            };

            _userFollowerServiceMock.Setup(u => u.FollowUser(1, 2)).Returns(Task.FromResult(new Response<string>(null, true, "User already followed by that user")));


            var controller = new UserFollowerController(_userFollowerServiceMock.Object);

            //act
            var result = await controller.FollowUser(1, 2);


            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var actual = (result as BadRequestObjectResult).Value;

            Assert.Equal("Already following this account", actual.ToString());

            //arrange 2
            _userFollowerServiceMock.Setup(u => u.FollowUser(1, 2)).Returns(Task.FromResult(new Response<string>(null, true, "user followed")));


            //act 2
            var result2 = await controller.FollowUser(1, 2);

            Assert.NotNull(result2);
            Assert.IsType<OkObjectResult>(result2);
            var actual2 = (result2 as OkObjectResult).Value;

            Assert.Equal("user followed", actual2.ToString());

        }
    }
}
