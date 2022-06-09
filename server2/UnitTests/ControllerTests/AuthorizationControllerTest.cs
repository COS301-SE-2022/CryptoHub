using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class AuthorizationControllerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public AuthorizationControllerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Login_User_ReturnsUser()
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

            var controllerAuth = new AuthorizationController(_userRepositoryMock.Object);

            //act
            var result = await controllerAuth.Login(user);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Response<User>>(actual);

            //arrange 2 Tests for null return (no user found)
            _userRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync((User)null);

            //act2
            var result2 = await controllerAuth.Login(user);

            Assert.NotNull(result2);
            Assert.IsType<BadRequestObjectResult>(result2.Result);

            var actual2 = (result2.Result as BadRequestObjectResult).Value;
            Assert.IsType<Response<User>>(actual2);
        }

        [Fact]
        public async Task Register_User_ReturnsUser()
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

            _userRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync((User)null);

            var controllerAuth = new AuthorizationController(_userRepositoryMock.Object);

            //act
            var result = await controllerAuth.Register(user);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Response<User>>(actual);

            //arrange 2 Tests for null return (user already registered)
            _userRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);

            //act2
            var result2 = await controllerAuth.Register(user);

            Assert.NotNull(result2);
            Assert.IsType<BadRequestObjectResult>(result2.Result);

            var actual2 = (result2.Result as BadRequestObjectResult).Value;
            Assert.IsType<Response<User>>(actual2);
        }
    }
}
