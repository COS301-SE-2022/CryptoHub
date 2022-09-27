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

        [Fact]
        public async Task GetPostByUserId_NoPosts()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetPostByUserId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var posts = await response.Content.ReadAsAsync<List<PostDTO>>();

            Assert.Empty(posts);
        }

        [Fact]
        public async Task GetPostByUserId_Posts()
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
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetPostByUserId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var posts = await response.Content.ReadAsAsync<List<PostDTO>>();

            Assert.Single(posts);
        }

        [Fact]
        public async Task AddPost()
        {
            //Arrange
            var testPost = new CreatePostDTO()
            {
                Post = "sample post",
                UserId = 1
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/AddPost", testPost);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetPostByUserId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var posts = await response.Content.ReadAsAsync<List<PostDTO>>();

            Assert.Single(posts);
            Assert.Equal(testPost.Post, posts.First().Content);
            Assert.Equal(testPost.UserId, posts.First().UserId);
        }

        //[Fact]
        //public async Task UpdatePost()
        //{
        //    //Arrange
        //    var testPost = new CreatePostDTO()
        //    {
        //        Post = "sample post",
        //        UserId = 1
        //    };
        //    var updatePost = new Post()
        //    {
        //        PostId = 1,
        //        Content = "Updated post",
        //        UserId = 1
        //    };

        //    await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/AddPost", testPost);

        //    //Act
        //    var response = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetPostByUserId/1");

        //    //Assert
        //    Assert.NotNull(response);
        //    Assert.Equal(200, (double)response.StatusCode);

        //    var posts = await response.Content.ReadAsAsync<List<PostDTO>>();

        //    Assert.Single(posts);
        //    Assert.Equal(testPost.Post, posts.First().Content);
        //    Assert.Equal(testPost.UserId, posts.First().UserId);

        //    //Arrange 2
        //    await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/UpdatePost", updatePost);

        //    //Act 2
        //    var responseUpdate = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetPostByUserId/1");

        //    //Assert 2
        //    Assert.NotNull(responseUpdate);
        //    Assert.Equal(200, (double)responseUpdate.StatusCode);

        //    var postsUpdate = await response.Content.ReadAsAsync<List<PostDTO>>();

        //    Assert.Single(postsUpdate);
        //    Assert.Equal(updatePost.Content, postsUpdate.First().Content);
        //    Assert.Equal(updatePost.UserId, postsUpdate.First().UserId);
        //}

        [Fact]
        public async Task Delete()
        {
            //Arrange
            var testPost = new CreatePostDTO()
            {
                Post = "sample post",
                UserId = 1
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/AddPost", testPost);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetPostByUserId/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var posts = await response.Content.ReadAsAsync<List<PostDTO>>();

            Assert.Single(posts);
            Assert.Equal(testPost.Post, posts.First().Content);
            Assert.Equal(testPost.UserId, posts.First().UserId);

            //Act
            var responseDelete = await _httpClient.DeleteAsync("http://localhost:7215/api/Post/Delete?id=1");

            //Assert
            Assert.NotNull(responseDelete);
            Assert.Equal(200, (double)responseDelete.StatusCode);

            //Act
            var responseGet = await _httpClient.GetAsync("http://localhost:7215/api/Post/GetPostByUserId/1");

            //Assert
            Assert.NotNull(responseGet);
            Assert.Equal(200, (double)responseGet.StatusCode);

            var postsGet = await responseGet.Content.ReadAsAsync<List<PostDTO>>();

            Assert.Empty(postsGet);
        }

        [Fact]
        public async Task ReportPost_Post()
        {
            //Arrange
            var testPost = new CreatePostDTO()
            {
                Post = "sample post",
                UserId = 1
            };
            var report = new PostReport()
            {
                UserId = 1,
                PostId = 1
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/AddPost", testPost);

            //Act
            var response = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/Report", report);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var reports = await response.Content.ReadAsAsync<PostReport>();

            Assert.NotNull(reports);
        }

        [Fact]
        public async Task ReportPost_NoPost()
        {
            //Arrange
            var report = new PostReport()
            {
                UserId = 1,
                PostId = 1
            };

            //Act
            var response = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Post/Report", report);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var reports = await response.Content.ReadAsAsync<PostReport>();

            Assert.NotNull(reports);
        }
    }
}
