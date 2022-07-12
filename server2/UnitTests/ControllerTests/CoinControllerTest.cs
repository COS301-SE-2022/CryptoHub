using BusinessLogic.Services.CoinService;
using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using Infrastructure.DTO.CoinDTOs;


namespace UnitTests.ControllerTests
{
    public class CoinControllerTest
    {
        private readonly Mock<ICoinRepository> _coinRepositoryMock;
        private readonly Mock<ICoinService> _coinServiceMock;


        public CoinControllerTest()
        {
            _coinServiceMock = new Mock<ICoinService>();
            _coinRepositoryMock = new Mock<ICoinRepository>();
        }

        [Fact]
        public async Task GetAllCoins_ListOfCoins_ReturnsListOfCoins()
        {
            //arrange
            List<CoinDTO> coins = new List<CoinDTO>
            {
                new CoinDTO
                {
                    CoinId = 1,
                    CoinName = "Coin1",
                },
                new CoinDTO
                {
                    CoinId = 2,
                    CoinName = "Coin2",
                },
                new CoinDTO
                {
                    CoinId = 3,
                    CoinName = "Coin3",
                }
            };

            _coinServiceMock.Setup(u => u.GetAllCoins()).ReturnsAsync(coins);

            var controller = new CoinController(_coinServiceMock.Object);

            //act
            var result = await controller.GetAllCoins();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<Coin>>(actual);
            Assert.Equal(3, (actual as List<Coin>).Count);
        }

        [Fact]
        public async Task UpdateCoin_Coin_ReturnsCoin()
        {
            //arrange
            var coin = new CoinDTO
            {
                CoinId = 1,
                CoinName = "Coin1",
            };

            _coinServiceMock.Setup(u => u.UpdateCoin(coin)).ReturnsAsync(coin);

            var controller = new CoinController(_coinServiceMock.Object);

            //act
            var result = await controller.UpdateCoin(coin);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Coin>(actual);

            //arrange
            _coinRepositoryMock.Setup(u => u.Update(It.IsAny<Expression<Func<Coin, bool>>>(), It.IsAny<Coin>())).ReturnsAsync((Coin)null);

            //act
            var result2 = await controller.UpdateCoin(coin);

            //assert
            Assert.Null(result2);
        }
    }
}
