using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class CoinHistoryControllerTest
    {
        private readonly Mock<ICoinHistoryRepository> __coinHistoryRepositoryMock;

        public CoinHistoryControllerTest()
        {
            __coinHistoryRepositoryMock = new Mock<ICoinHistoryRepository>();
        }

        [Fact]
        public async Task GetAllCoinHistory_ListOfCoinHistory_ReturnsListOfCoinHistory()
        {
            //arrange
            List<CoinHistory> coinHistories = new List<CoinHistory>
            {
                new CoinHistory
                {
                    HistoryId = 1,
                    CoinId = 1,
                    Rank = 1,
                    Timestamp = null,
                    TradingPriceUsd = 1,
                    PercentageChange = 1,
                    Supply = 1,
                    MaxSupply = 1,
                    MarketCapUsd = 1
                },
                new CoinHistory
                {
                    HistoryId = 2,
                    CoinId = 2,
                    Rank = 2,
                    Timestamp = null,
                    TradingPriceUsd = 2,
                    PercentageChange = 2,
                    Supply = 2,
                    MaxSupply = 2,
                    MarketCapUsd = 2
                },
                new CoinHistory
                {
                    HistoryId = 3,
                    CoinId = 3,
                    Rank = 3,
                    Timestamp = null,
                    TradingPriceUsd = 3,
                    PercentageChange = 3,
                    Supply = 3,
                    MaxSupply = 3,
                    MarketCapUsd = 3
                }
            };

            __coinHistoryRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(coinHistories);

            var controller = new CoinHistoryController(__coinHistoryRepositoryMock.Object);

            //act
            var result = await controller.GetAllCoinHistory();


            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);


            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<CoinHistory>>(actual);
            Assert.Equal(3, (actual as List<CoinHistory>).Count);


        }
    }
}
