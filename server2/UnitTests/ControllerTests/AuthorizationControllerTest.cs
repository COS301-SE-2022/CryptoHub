using BusinessLogic.Services.AuthorizationService;
using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using Infrastructure.DTO.UserDTOs;

namespace UnitTests.ControllerTests
{
    public class AuthorizationControllerTest
    {
        private readonly Mock<IAuthorizationService> _authorizationServiceMock;

        public AuthorizationControllerTest()
        {
            _authorizationServiceMock = new Mock<IAuthorizationService>();
        }

        [Fact]
        public async Task Login_User_ReturnsUser()
        {
            //arrange
            var login = new LoginDTO
            {
                Email = "johndoe@gmail.com",
                Password = "1234"
            };

            var jwt = new JWT("abc");
            var token = CreateToken(loginUser, role.Name);
            var success = new Response<JWT>(token, false, "logged in");

            _authorizationServiceMock.Setup(u => u.Login(login)).Returns(Task.FromResult(success));

            var controllerAuth = new AuthorizationController(_authorizationServiceMock.Object);

            //act
            var result = await controllerAuth.Login(login);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Response<User>>(actual);

            //arrange 2 Tests for null return (no user found)
            //_userRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync((User)null);
            _authorizationServiceMock.Setup(u => u.Login(login)).Returns((Task<Response<JWT>>)null);


            //act2
            var result2 = await controllerAuth.Login(login);

            //Assert 2
            Assert.NotNull(result2);
            Assert.IsType<BadRequestObjectResult>(result2.Result);

            var actual2 = (result2.Result as BadRequestObjectResult).Value;
            Assert.IsType<Response<User>>(actual2);

            //arrange 3 Tests for when passwords dont match
            var user2 = new LoginDTO
            {
                Email = "johndoe@gmail.com",
                Password = "4321"
            };

            _authorizationServiceMock.Setup(u => u.Login(login));

            //act 3
            var result3 = await controllerAuth.Login(user2);

            //assert 3
            Assert.NotNull(result3);
            Assert.IsType<BadRequestObjectResult>(result3.Result);

            var actual3 = (result2.Result as BadRequestObjectResult).Value;
            Assert.IsType<Response<User>>(actual3);
        }

        [Fact]
        public async Task Register_User_ReturnsUser()
        {
            //Base arrange
            var register = new RegisterDTO
            {
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john",
                Password = "1234"
            };

            //Arrange Tests for not null return (user already registered)
            _authorizationServiceMock.Setup(u => u.Register(register));
            var controllerAuth = new AuthorizationController(_authorizationServiceMock.Object);

            //Act
            var result2 = await controllerAuth.Register(register);

            //Assert
            Assert.NotNull(result2);
            Assert.IsType<BadRequestObjectResult>(result2.Result);

            var actual2 = (result2.Result as BadRequestObjectResult).Value;
            Assert.IsType<Response<User>>(actual2);

            //Arrange Tests for null return (user not yet registered)
            _authorizationServiceMock.Setup(u => u.Register(register)).Returns((Task<Response<JWT>>)null);

            //Act
            var result = await controllerAuth.Register(register);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Response<User>>(actual);
        }
    }
}
