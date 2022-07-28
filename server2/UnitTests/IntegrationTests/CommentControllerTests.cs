using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.PostDTO;
using Infrastructure.DTO.CommentDTOs;
using System.Net.Http;

namespace UnitTests.IntegrationTests
{
    public class CommentControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        //private CryptoHubDBContext _context;


        public CommentControllerTests()
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
        public async Task GetCommentByUserId_NoComments()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Comment/GetCommentByUserId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);
        }

        [Fact]
        public async Task GetCommentByUserId_Comments()
        {
            //Arrange
            var testComment = new CommentDTO()
            {
                CommentId = 1,
                UserId = 1,
                PostId = 1,
                Content = "sample comment 1"
            };
            var testComment2 = new CommentDTO()
            {
                CommentId = 2,
                UserId = 2,
                PostId = 2,
                Content = "sample comment 2"
            };
            var testComment3 = new CommentDTO()
            {
                CommentId = 3,
                UserId = 3,
                PostId = 3,
                Content = "sample comment 3"
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Comment/GetCommentByUserId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var comments = await response.Content.ReadAsAsync<Comment>();

            Assert.NotNull(comments);
        }

        [Fact]
        public async Task GetCommentByPostId_NoComments()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Comment/GetCommentByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var comments = await response.Content.ReadAsAsync<List<Comment>>();

            Assert.Empty(comments);
        }

        [Fact]
        public async Task GetCommentByPostId_Comments()
        {
            //Arrange
            var testComment = new CommentDTO()
            {
                CommentId = 1,
                UserId = 1,
                PostId = 1,
                Content = "sample comment 1"
            };
            var testComment2 = new CommentDTO()
            {
                CommentId = 2,
                UserId = 2,
                PostId = 2,
                Content = "sample comment 2"
            };
            var testComment3 = new CommentDTO()
            {
                CommentId = 3,
                UserId = 3,
                PostId = 3,
                Content = "sample comment 3"
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Comment/AddComment", testComment3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Comment/GetCommentByPostId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var comments = await response.Content.ReadAsAsync<List<Comment>>();

            Assert.Single(comments);
        }
    }
}