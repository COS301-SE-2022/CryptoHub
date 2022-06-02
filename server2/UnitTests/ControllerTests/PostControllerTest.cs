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
        public async Task GetPostByUserId_ListOfPosts_ReturnsListOfPosts()
        {
            //arrange
            List<Post> users = new List<Post>
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

            var post = new Post
            {
                PostId = 1,
                Post1 = "Post 1",
                UserId = 1
            };

            _postRepositoryMock.Setup(x => x.FindOne(It.IsAny<Expression<Func<Post, bool>>>())).ReturnsAsync(post);

            var controller = new PostController(_postRepositoryMock.Object);

            //act
            var result = await controller.GetPostByUserId(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<User>(actual);
        }

    }
}
