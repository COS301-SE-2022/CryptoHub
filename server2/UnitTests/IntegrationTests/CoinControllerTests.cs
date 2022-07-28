using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.PostDTO;
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
    }
}
