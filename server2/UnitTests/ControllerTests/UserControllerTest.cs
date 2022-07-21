using CryptoHubAPI.Controllers;
using BusinessLogic.Services.UserService;
using BusinessLogic.Services.UserCoinService;
using BusinessLogic.Services.SearchService;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using Infrastructure.DTO.UserDTOs;

namespace UnitTests.ControllerTests
{
    public class UserControllerTest
    {

        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IUserCoinService> _userCoinServiceMock;
        private readonly Mock<ISearchService> _searchServiceMock;

        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _userCoinServiceMock = new Mock<IUserCoinService>();
            _searchServiceMock = new Mock<ISearchService>();
        }


        [Fact]
        public async Task GetAllUsers_ListOfUsers_ReturnsListOfUsers()
        {
            //arrange
            List<UserDTO> users = new List<UserDTO>
            {
                new UserDTO
                {
                    UserId = 1,
                    Email = "johndoe@gmail.com",
                    Firstname = "john",
                    Lastname = "doe",
                    Username = "john"
                },
                new UserDTO
                {
                    UserId = 2,
                    Email = "elonmusk@gmail.com",
                    Firstname = "elon",
                    Lastname = "musk",
                    Username = "elon"
                },
                new UserDTO
                {
                    UserId = 3,
                    Email = "billgates@gmail.com",
                    Firstname = "bill",
                    Lastname = "gates",
                    Username = "bill"
                }
            };

            _userServiceMock.Setup(u => u.GetAllUsers()).Returns(Task.FromResult(users));

            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

            //act
            var result = await controller.GetAllUsers();


            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);


            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<User>>(actual);
            Assert.Equal(3, (actual as List<User>).Count);


        }

        [Fact]
        public async Task GetUserById_UserId_ReturnsUserOfId()
        {
            //arrange
            var user = new UserDTO
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john"
            };

            _userServiceMock.Setup(u => u.GetById(user.UserId)).Returns(Task.FromResult(user));


            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

            //act
            var result = await controller.GetUserById(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<User>(actual);
        }

        [Fact]
        public async Task GetUserByEmail_UserEmail_ReturnsUserOfEmail()
        {
            //arrange
            var user = new UserDTO
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john"
            };

            _userServiceMock.Setup(u => u.GetUserByEmail(user.Email)).Returns(Task.FromResult(user));

            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

            //act
            var result = await controller.GetUserByEmail(user.Email);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<User>(actual);
        }

        [Fact]
        public async Task AddUser_User_ReturnsUser()
        {
            //arrange
            var user = new User
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john",
                Password = "1234"
            };
            var userDTO = new UserDTO
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john"
            };
            _userServiceMock.Setup(u => u.AddUser(user)).Returns(Task.FromResult(userDTO));

            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

            //act
            var result = await controller.AddUser(user);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<User>(actual);
        }

        [Fact]
        public async Task UpdateUser_User_ReturnsUser()
        {
            //arrange
            var user = new User
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john",
                Password = "1234"
            };
            var userDTO = new UserDTO
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john"
            };

            _userServiceMock.Setup(u => u.UpateUser(user)).Returns(Task.FromResult(userDTO));

            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

            //act
            var result = await controller.UpdateUser(user);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<User>(actual);
        }

        [Fact]
        public async Task Delete_User_None()
        {
            //arrange
            var user = new User
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john",
                Password = "1234"
            };

            _userServiceMock.Setup(u => u.Delete(user.UserId)).Returns(Task.FromResult(user));

            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

            //act
            var result = await controller.Delete(user.UserId);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}