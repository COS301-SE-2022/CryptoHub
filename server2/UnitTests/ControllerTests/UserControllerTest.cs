/*using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class UserControllerTest
    {

        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserControllerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }


        [Fact]
        public async Task GetAllUsers_ListOfUsers_ReturnsListOfUsers()
        {
            //arrange
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

            _userRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(users);

            var controller = new UserController(_userRepositoryMock.Object);

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
            var user = new User
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john",
                Password = "1234"
            };

            _userRepositoryMock.Setup(u => u.GetById(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);

            var controller = new UserController(_userRepositoryMock.Object);

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
            var user = new User
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john",
                Password = "1234"
            };

            _userRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);

            var controller = new UserController(_userRepositoryMock.Object);

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
            _userRepositoryMock.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(user);

            var controller = new UserController(_userRepositoryMock.Object);

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

            _userRepositoryMock.Setup(u => u.Update(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<User>())).ReturnsAsync(user);

            var controller = new UserController(_userRepositoryMock.Object);

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

            _userRepositoryMock.Setup(u => u.DeleteOne(It.IsAny<Expression<Func<User, bool>>>()));

            var controller = new UserController(_userRepositoryMock.Object);

            //act
            var result = await controller.Delete(user.UserId);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}*/