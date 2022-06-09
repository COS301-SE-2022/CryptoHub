using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class PostControllerTest
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;

        public PostControllerTest()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
        }
        [Fact]
        public async Task GetAllPosts_ListOfPosts_ReturnsListOfPosts()
        {
            //arrange
            List<Post> posts = new List<Post>
            {
                new Post
                {
                    PostId = 1,
                    Post1 = "Post 1",
                    UserId = 1
                },
                new Post
                {
                    PostId = 2,
                    Post1 = "Post 2",
                    UserId = 2
                },
                new Post
                {
                    PostId = 3,
                    Post1 = "Post 3",
                    UserId = 3
                }
            };

            _postRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(posts);

            var controller = new PostController(_postRepositoryMock.Object);

            //act
            var result = await controller.GetAllPosts();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<Post>>(actual);
            Assert.Equal(3, (actual as List<Post>).Count);
        }

        [Fact]
        public async Task GetPostsByUserId_UserId_ReturnsPostsOfId()
        {
            //arrange
            List<Post> posts = new List<Post>
            {
                new Post
                {
                    PostId = 1,
                    Post1 = "Post 1",
                    UserId = 1
                }
            };

            _postRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<Post, bool>>>())).ReturnsAsync(posts);
            //_postRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(posts);

            var controller = new PostController(_postRepositoryMock.Object);

            //act
            var result = await controller.GetPostByUserId(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<Post>>(actual);
            Assert.Equal(1, (actual as List<Post>).Count);
        }

        [Fact]
        public async Task AddPost_Post_ReturnsPost()
        {
            var post = new Post
            {
                PostId = 1,
                Post1 = "Post 1",
                UserId = 1
            };
            _postRepositoryMock.Setup(u => u.Add(It.IsAny<Post>())).ReturnsAsync(post);

            var controller = new PostController(_postRepositoryMock.Object);

            //act
            var result = await controller.AddPost(post);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Post>(actual);
        }

        [Fact]
        public async Task UpdatePost_Post_ReturnsPost()
        {
            var post = new Post
            {
                PostId = 1,
                Post1 = "Post 1",
                UserId = 1
            };

            _postRepositoryMock.Setup(u => u.Update(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<Post>())).ReturnsAsync(post);

            var controller = new PostController(_postRepositoryMock.Object);

            //act
            var result = await controller.UpdatePost(post);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Post>(actual);
        }

        [Fact]
        public async Task Delete_Post_None()
        {
            var post = new Post
            {
                PostId = 1,
                Post1 = "Post 1",
                UserId = 1
            };

            _postRepositoryMock.Setup(u => u.DeleteOne(It.IsAny<Expression<Func<Post, bool>>>()));

            var controller = new PostController(_postRepositoryMock.Object);

            //act
            var result = await controller.Delete(post.PostId);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
