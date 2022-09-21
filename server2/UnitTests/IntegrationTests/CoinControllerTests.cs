using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.CoinDTOs;
using Infrastructure.DTO.UserDTOs;
using Infrastructure.DTO.UserCoinDTOs;
using Infrastructure.DTO.UserFollowerDTOs;
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
        public async Task GetCoin_Coin()
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

            var coins = await response.Content.ReadAsAsync<Coin>();

            Assert.NotNull(coins);
        }

        //[Fact]
        //public async Task UpdateCoin()
        //{
        //    //Arrange
        //    var testCoin = new CoinDTO()
        //    {
        //        CoinId = 1,
        //        CoinName = "TestCoin1",
        //        ImageUrl = "TestURL"
        //    };
        //    var testCoinUpdate = new CoinDTO()
        //    {
        //        CoinId = 1,
        //        CoinName = "TestCoin1",
        //        ImageUrl = "Updated URL"
        //    };

        //    await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin);

        //    //Act
        //    var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoin/1");

        //    //Assert
        //    Assert.NotNull(response);
        //    Assert.Equal(200, (double)response.StatusCode);

        //    var coins = await response.Content.ReadAsAsync<CoinDTO>();

        //    //Assert.Single(coins);
        //    Assert.Equal(testCoin.CoinId, coins.CoinId);
        //    Assert.Equal(testCoin.CoinName, coins.CoinName);
        //    Assert.Equal(testCoin.ImageUrl, coins.ImageUrl);

        //    //Act 2
        //    await _httpClient.PutAsJsonAsync("http://localhost:7215/api/Coin/UpdateCoin", testCoinUpdate);
        //    var responseUpdate = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllCoins");

        //    //Assert 2
        //    Assert.NotNull(responseUpdate);
        //    Assert.Equal(200, (double)responseUpdate.StatusCode);

        //    var commentsUpdated = await responseUpdate.Content.ReadAsAsync<List<CoinDTO>>();

        //    Assert.Equal(testCoinUpdate.CoinId, commentsUpdated.First().CoinId);
        //    Assert.Equal(testCoinUpdate.CoinName, commentsUpdated.First().CoinName);
        //    Assert.Equal(testCoinUpdate.ImageUrl, commentsUpdated.First().ImageUrl);
        //}

        [Fact]
        public async Task RateCoin_NoCoin()
        {
            //Arrange
            var testCoin = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };

            //Act
            var response = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/RateCoin/1/TestCoin1/2", testCoin);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(400, (double)response.StatusCode);
        }

        //[Fact]
        //public async Task RateCoin_Coin()
        //{
        //    //Arrange
        //    var testCoin = new CoinDTO()
        //    {
        //        CoinId = 1,
        //        CoinName = "TestCoin1",
        //        ImageUrl = "TestURL"
        //    };
        //    var testUser = new User()
        //    {
        //        UserId = 1,
        //        Firstname = "test1",
        //        Lastname = "user1",
        //        Username = "user1",
        //        Email = "test@gmail.com",
        //        Password = "1234"
        //    };

        //    var userAdd = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
        //    var coinAdd = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin);

        //    Assert.Equal(200, (double)userAdd.StatusCode);

        //    //Act
        //    var response = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/RateCoin/1/TestCoin1/2", testCoin);

        //    //Assert
        //    Assert.NotNull(response);
        //    //Assert.Equal(200, (double)response.StatusCode);
        //}

        [Fact]
        public async Task GetAllUserCoins_NoUserCoins()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllUserCoins");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.Empty(coins);
        }

        [Fact]
        public async Task GetAllUserCoins_UserCoins()
        {
            //Arrange
            var testCoin1 = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testCoin2 = new CoinDTO()
            {
                CoinId = 2,
                CoinName = "TestCoin2",
                ImageUrl = "TestURL"
            };
            var testUserCoin = new UserCoinDTO()
            {
                UserId = 1,
                CoinId = testCoin1.CoinId,
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin1", testUserCoin);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin2", testUserCoin);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllUserCoins");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.NotEmpty(coins);
            Assert.Equal(2, coins.Count());
        }

        [Fact]
        public async Task FollowCoin_UserCoins()
        {
            //Arrange
            var testCoin1 = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testUserCoin = new UserCoinDTO()
            {
                UserId = 1,
                CoinId = testCoin1.CoinId,
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin1", testUserCoin);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllUserCoins");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.NotEmpty(coins);
            Assert.Equal(1, coins.Count());
        }

        [Fact]
        public async Task UnfollowCoin_UserCoins()
        {
            //Arrange
            var testCoin1 = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testCoin2 = new CoinDTO()
            {
                CoinId = 2,
                CoinName = "TestCoin2",
                ImageUrl = "TestURL"
            };
            var testUserCoin = new UserCoinDTO()
            {
                UserId = 1,
                CoinId = testCoin1.CoinId,
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin1", testUserCoin);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin2", testUserCoin);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllUserCoins");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.NotEmpty(coins);
            Assert.Equal(2, coins.Count());

            //Arrange 2
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/UnfollowCoin/1/TestCoin2", testUserCoin);

            //Act 2
            var response2 = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetAllUserCoins");

            //Assert 2
            Assert.NotNull(response2);
            Assert.Equal(200, (double)response2.StatusCode);

            var coins2 = await response2.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.NotEmpty(coins2);
            Assert.Equal(1, coins2.Count());

        }

        [Fact]
        public async Task GetCoinFollowCount_Coins()
        {
            //Arrange
            var testCoin1 = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testUserCoin1 = new UserCoinDTO()
            {
                UserId = 1,
                CoinId = testCoin1.CoinId,
            };
            var testUserCoin2 = new UserCoinDTO()
            {
                UserId = 2,
                CoinId = testCoin1.CoinId,
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin1", testUserCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/2/TestCoin1", testUserCoin2);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoinFollowCount/TestCoin1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coin = await response.Content.ReadAsAsync<UserCoinDTO>();

            //var actual = (response as OkObjectResult).Value;

            Assert.NotNull(coin);

            //var actual = (coin.Result as OkObjectResult).Value;
            //Assert.IsType<List<UserCoinDTO>>(actual);
            //Assert.Equal(3, (actual as List<Like>).Count);
        }

        [Fact]
        public async Task GetCoinsFollowers_CoinsFollowers()
        {
            //Arrange
            var testCoin1 = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testUserCoin1 = new UserCoinDTO()
            {
                UserId = 1,
                CoinId = testCoin1.CoinId,
            };
            var testUserCoin2 = new UserCoinDTO()
            {
                UserId = 2,
                CoinId = testCoin1.CoinId,
            };
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin1", testUserCoin1);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoinsFollowers/TestCoin1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.NotEmpty(coins);
            Assert.Equal(1, coins.Count());
        }

        [Fact]
        public async Task GetCoinsFollowedByUser_CoinsFollowed()
        {
            //Arrange
            var testCoin1 = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testCoin2 = new CoinDTO()
            {
                CoinId = 2,
                CoinName = "TestCoin2",
                ImageUrl = "TestURL"
            };
            var testUserCoin1 = new UserCoinDTO()
            {
                UserId = 1,
                CoinId = testCoin1.CoinId,
            };
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };

            var add = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin1", testUserCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/1/TestCoin2", testUserCoin1);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoinsFollowedByUser/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.NotEmpty(coins);
            Assert.Equal(2, coins.Count());
        }

        [Fact]
        public async Task GetCoinsFollowedByUser_NoCoinsFollowed()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/GetCoinsFollowedByUser/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.Empty(coins);
        }

        [Fact]
        public async Task SearchCoin_NoCoins()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/SearchCoin/1/Test");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.Empty(coins);
        }

        [Fact]
        public async Task SearchCoin_Coins()
        {
            //Arrange
            var testCoin1 = new CoinDTO()
            {
                CoinId = 1,
                CoinName = "TestCoin1",
                ImageUrl = "TestURL"
            };
            var testUser1 = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };
            var testUser2 = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };
            var testUserCoin1 = new UserCoinDTO()
            {
                UserId = 1,
                CoinId = testCoin1.CoinId,
            };
            var testUserFollow = new UserFollowerDTO()
            {
                Id = 1,
                UserId = 1,
                FollowId = 2,
                FollowDate = DateTime.Now,
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/AddCoin", testCoin1);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/UserFollower/FollowUser/2/1", testUserFollow);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Coin/FollowCoin/2/TestCoin1", testUserCoin1);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Coin/SearchCoin/1/Test");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var coins = await response.Content.ReadAsAsync<List<UserCoinDTO>>();

            Assert.NotEmpty(coins);
        }

        [Fact]
        public async Task SearchCoin_Coins()
        {
        }
    }
}
