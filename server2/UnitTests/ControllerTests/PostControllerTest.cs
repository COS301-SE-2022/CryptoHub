using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class PostControllerTest
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;

        public PostControllerTest()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
        }
    }
}
