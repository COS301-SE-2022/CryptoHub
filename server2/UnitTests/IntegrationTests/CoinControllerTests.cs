using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.CoinDTOs;
using System.Net.Http;

namespace UnitTests.IntegrationTests
{
    public class CoinControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        //private CryptoHubDBContext _context;


        public CoinControllerTests()
        {
            var dbname = Guid.NewGuid().ToString();
            var appFactory = new WebApplicationFactory<Program>()
                             .WithWebHostBuilder(builder =>
                             {
                                 builder.ConfigureServices(
                                     services =>
                                     {
                                         var descriptor = services.SingleOrDefault(
                                             d => d.ServiceType == typeof(DbContextOptions<CryptoHubDBContext>));

                                         if (descriptor != null)
                                         {
                                             services.Remove(descriptor);
                                         }
                                         services.AddDbContext<CryptoHubDBContext>(
                                             options =>
                                             {
                                                 options.UseInMemoryDatabase(dbname);
                                             });
                                     });
                             });

            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllCoins_NoCoins()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllCoins");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<Coin>>();

            Assert.Empty(coins);
        }

        [Fact]
        public async Task GetAllCoins_Coins()
        {
            //Arrange
            var testCoin = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testCoin1 = new CoinDTO()
            {
                CoinId = 2,
                CoinName = "TestCoin2",
                ImageUrl = "TestURL"
            };
            var testCoin2 = new CoinDTO()
            {
                CoinId = 2,
                CoinName = "TestCoin3",
                ImageUrl = "TestURL"
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin2);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllCoins");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var comments = await response.Content.ReadAsAsync<List<Coin>>();

            Assert.NotNull(comments);
            Assert.NotEmpty(comments);
        }

        [Fact]
        public async Task AddCoin()
        {
            //Arrange
            var testCoin = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };


            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoin/1");
            //var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllCoins");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<CoinDTO>();

            //Assert.Single(coins);
            Assert.Equal(testCoin.CoinId, coins.CoinId);
            Assert.Equal(testCoin.CoinName, coins.CoinName);
            Assert.Equal(testCoin.ImageUrl, coins.ImageUrl);
        }

        [Fact]
        public async Task GetCoin_NoCoin()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoin/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);
        }

        [Fact]
        public async Task GetCoins_Coin()
        {
            //Arrange
            var testCoin = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoin/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var comments = await response.Content.ReadAsAsync<Coin>();

            Assert.NotNull(comments);
        }

    }
}
