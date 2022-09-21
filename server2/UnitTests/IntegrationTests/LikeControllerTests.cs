using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.LikeDTOs;
using System.Net.Http;

namespace UnitTests.IntegrationTests
{
    public class LikeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        //private CryptoHubDBContext _context;


        public LikeControllerTests()
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
        public async Task AddLike()
        {
            //Arrange
            var testLike = new LikeDTO()
            {
                LikeId = 1,
                UserId = 1,
                PostId = 1,
                CommentId = 1,
            };


            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<List<LikeDTO>>();

            Assert.Single(likes);
            Assert.Equal(testLike.LikeId, likes.First().LikeId);
            Assert.Equal(testLike.UserId, likes.First().UserId);
            Assert.Equal(testLike.PostId, likes.First().PostId);
            Assert.Equal(testLike.CommentId, likes.First().CommentId);

        }

        [Fact]
        public async Task UpdateLike()
        {
            //Arrange
            var testLike = new LikeDTO()
            {
                LikeId = 1,
                UserId = 1,
                PostId = 1,
                CommentId = 1,
            };
            var testLikeUpdate = new LikeDTO()
            {
                LikeId = 1,
                UserId = 1,
                PostId = 1,
                CommentId = 1,
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<List<LikeDTO>>();

            Assert.Single(likes);
            Assert.Equal(testLike.LikeId, likes.First().LikeId);
            Assert.Equal(testLike.UserId, likes.First().UserId);
            Assert.Equal(testLike.PostId, likes.First().PostId);
            Assert.Equal(testLike.CommentId, likes.First().CommentId);

            //Act 2
            await _httpClient.PutAsJsonAsync("http://localhost:7215/api/like/UpdateLike", testLikeUpdate);
            var response2 = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByPostId/1");

            //Assert
            Assert.NotNull(response2);
            Assert.Equal(200, (double)response2.StatusCode);

            var likes2 = await response2.Content.ReadAsAsync<List<LikeDTO>>();

            Assert.Single(likes2);
            Assert.Equal(testLike.LikeId, likes2.First().LikeId);
            Assert.Equal(testLike.UserId, likes2.First().UserId);
            Assert.Equal(testLike.PostId, likes2.First().PostId);
            Assert.Equal(testLike.CommentId, likes2.First().CommentId);
        }

        [Fact]
        public async Task GetLikeByPostId_NoLikes()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<List<Like>>();

            Assert.Empty(likes);
        }

        [Fact]
        public async Task GetLikeByPostId_Likes()
        {
            //Arrange
            var testLike = new LikeDTO()
            {
                LikeId = 1,
                UserId = 1,
                PostId = 1,
                CommentId = 1
            };
            var testLike2 = new LikeDTO()
            {
                LikeId = 2,
                UserId = 2,
                PostId = 2,
                CommentId = 2
            };
            var testLike3 = new LikeDTO()
            {
                LikeId = 3,
                UserId = 3,
                PostId = 3,
                CommentId = 3
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<List<LikeDTO>>();

            Assert.Single(likes);
        }

        [Fact]
        public async Task GetLikeByCommentId_NoLikes()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByCommentId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<List<Like>>();

            Assert.Empty(likes);
        }

        [Fact]
        public async Task GetLikeByCommentId_Likes()
        {
            //Arrange
            var testLike = new LikeDTO()
            {
                LikeId = 1,
                UserId = 1,
                PostId = 1,
                CommentId = 1
            };
            var testLike2 = new LikeDTO()
            {
                LikeId = 2,
                UserId = 2,
                PostId = 2,
                CommentId = 2
            };
            var testLike3 = new LikeDTO()
            {
                LikeId = 3,
                UserId = 3,
                PostId = 3,
                CommentId = 3
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByCommentId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<List<LikeDTO>>();

            Assert.Single(likes);
        }

        [Fact]
        public async Task GetLikeBy_NoLikes()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeBy/1/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);
        }

        [Fact]
        public async Task GetLikeBy_Likes()
        {
            //Arrange
            var testLike = new LikeDTO()
            {
                LikeId = 1,
                UserId = 1,
                PostId = 1,
                CommentId = 1
            };
            var testLike2 = new LikeDTO()
            {
                LikeId = 2,
                UserId = 2,
                PostId = 2,
                CommentId = 2
            };
            var testLike3 = new LikeDTO()
            {
                LikeId = 3,
                UserId = 3,
                PostId = 3,
                CommentId = 3
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeBy/1/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<LikeDTO>();

            Assert.NotNull(likes);
        }

        [Fact]
        public async Task Delete()
        {
            //Arrange
            var testLike = new LikeDTO()
            {
                LikeId = 1,
                UserId = 1,
                PostId = 1,
                CommentId = 1
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Like/AddLike", testLike);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var likes = await response.Content.ReadAsAsync<List<LikeDTO>>();

            Assert.Single(likes);

            //Act
            var responseDelete = await _httpClient.DeleteAsync("http://localhost:7215/api/Like/Delete/1/1");

            //Assert
            Assert.NotNull(responseDelete);
            Assert.Equal(200, (double)responseDelete.StatusCode);

            //Act
            var responseGet = await _httpClient.GetAsync("http://localhost:7215/api/Like/GetLikeByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);
        }
    }
}
