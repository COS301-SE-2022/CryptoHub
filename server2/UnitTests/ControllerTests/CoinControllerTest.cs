using BusinessLogic.Services.CoinService;
using BusinessLogic.Services.CoinRatingService;
using BusinessLogic.Services.UserCoinService;
using BusinessLogic.Services.SearchService;
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
        private readonly Mock<ICoinRatingService> _coinRatingServiceMock;
        private readonly Mock<IUserCoinService> _userCoinServiceMock;
        private readonly Mock<ICoinService> _coinServiceMock;
        private readonly Mock<ISearchService> _searchServiceMock;


        public CoinControllerTest()
        {
            _coinServiceMock = new Mock<ICoinService>();
            _userCoinServiceMock = new Mock<IUserCoinService>();
            _coinRatingServiceMock = new Mock<ICoinRatingService>();
            _searchServiceMock = new Mock<ISearchService>();
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

            var controller = new CoinController(_coinServiceMock.Object, _coinRatingServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

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

            var controller = new CoinController(_coinServiceMock.Object, _coinRatingServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

            //act
            var result = await controller.UpdateCoin(coin);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Coin>(actual);

            //arrange
            _coinServiceMock.Setup(u => u.UpdateCoin(coin)).ReturnsAsync(coin);

            //act
            var result2 = await controller.UpdateCoin(coin);

            //assert
            Assert.Null(result2);
        }
    }
}
