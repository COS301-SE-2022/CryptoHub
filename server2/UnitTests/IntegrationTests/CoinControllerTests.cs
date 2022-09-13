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

        //[Fact]
        //public async Task GetAllCoins_Coins()
        //{
        //    //Arrange
        //    var testComment = new CoinDTO()
        //    {
        //        CoinId = 1,
        //        CoinName = "TestCoin1",
        //        ImageUrl = "TestURL"
        //    };
        //    var testComment1 = new CoinDTO()
        //    {
        //        CoinId = 2,
        //        CoinName = "TestCoin2",
        //        ImageUrl = "TestURL"
        //    };
        //    var testComment2 = new CoinDTO()
        //    {
        //        CoinId = 2,
        //        CoinName = "TestCoin3",
        //        ImageUrl = "TestURL"
        //    };

        //    //Act
        //    await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testComment);
        //    await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment1);
        //    await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment2);

        //    //Assert
        //    Assert.NotNull(response);
        //    Assert.Equal(200, (double)response.StatusCode);

        //    var comments = await response.Content.ReadAsAsync<Comment>();

        //    Assert.NotNull(comments);
        //}

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

            var coins = await response.Content.ReadAsAsync<List<CoinDTO>>();

            Assert.Single(coins);
            Assert.Equal(testCoin.CoinId, coins.First().CoinId);
            Assert.Equal(testCoin.CoinName, coins.First().CoinName);
            Assert.Equal(testCoin.ImageUrl, coins.First().ImageUrl);
        }

    }
}
