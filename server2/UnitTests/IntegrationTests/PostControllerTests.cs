using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.PostDTO;

namespace UnitTests.IntegrationTests
{
    public class PostControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        //private CryptoHubDBContext _context;


        public PostControllerTests()
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
        public async Task GetAllPosts_NoPosts()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetAllPosts");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var posts = await response.Content.ReadAsAsync<List<PostDTO>>();

            Assert.Empty(posts);
        }

        [Fact]
        public async Task GetAllPosts_Posts()
        {
            //Arrange
            var testPost = new CreatePostDTO()
            {
                Post = "sample post",
                UserId = 1
            };
            var testPost2 = new CreatePostDTO()
            {
                Post = "sample post2",
                UserId = 2
            };
            var testPost3 = new CreatePostDTO()
            {
                Post = "sample post3",
                UserId = 3
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/AddPost", testPost);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/AddPost", testPost2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/AddPost", testPost3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetAllPosts");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var posts = await response.Content.ReadAsAsync<List<User>>();

            Assert.Equal(3, posts.Count);
        }
    }
}
