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

            _userRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users[0]);

            var controller = new AuthorizationController(_userRepositoryMock.Object);

            //act
            var result = await controller.Login(users[0]);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Response<User>>(actual);
        }
    }
}
