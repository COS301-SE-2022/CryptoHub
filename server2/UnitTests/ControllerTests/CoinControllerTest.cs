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

        [Fact]
        public async Task GetAllCoins_ListOfCoins_ReturnsListOfCoins()
        {
            //arrange
            List<Coin> coins = new List<Coin>
            {
                new Coin
                {
                    CoinId = 1,
                    CoinName = "Coin1",
                    Symbol = "CN1",
                    Rank = 1,
                    TradingPriceUsd = 1,
                    PercentageChange = 1,
                    Supply = 1,
                    MaxSupply = 10,
                    MarketCapUsd = 100
                },
                new Coin
                {
                    CoinId = 2,
                    CoinName = "Coin2",
                    Symbol = "CN2",
                    Rank = 2,
                    TradingPriceUsd = 2,
                    PercentageChange = 2,
                    Supply = 2,
                    MaxSupply = 20,
                    MarketCapUsd = 200
                },
                new Coin
                {
                    CoinId = 3,
                    CoinName = "Coin3",
                    Symbol = "CN3",
                    Rank = 3,
                    TradingPriceUsd = 3,
                    PercentageChange = 3,
                    Supply = 3,
                    MaxSupply = 30,
                    MarketCapUsd = 300
                }
            };

            _coinRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(coins);

            var controller = new CoinController(_coinRepositoryMock.Object);

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
            var coin = new Coin
            {
                CoinId = 1,
                CoinName = "Coin1",
                Symbol = "CN1",
                Rank = 1,
                TradingPriceUsd = 1,
                PercentageChange = 1,
                Supply = 1,
                MaxSupply = 10,
                MarketCapUsd = 100
            };

            _coinRepositoryMock.Setup(u => u.Update(It.IsAny<Expression<Func<Coin, bool>>>(), It.IsAny<Coin>())).ReturnsAsync(coin);

            var controller = new CoinController(_coinRepositoryMock.Object);

            //act
            var result = await controller.UpdateCoin(coin);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Coin>(actual);
        }
    }
}
