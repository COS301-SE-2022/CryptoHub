using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class CoinControllerTest
    {
        private readonly Mock<ICoinRepository> _coinRepositoryMock;

        public CoinControllerTest()
        {
            _coinRepositoryMock = new Mock<ICoinRepository>();
        }


    }
}
