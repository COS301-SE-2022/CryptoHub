using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class CommentControllerTest
    {
        private readonly Mock<ICommentRepository> _commentRepositoryMock;

        public CommentControllerTest()
        {
            _commentRepositoryMock = new Mock<ICommentRepository>();
        }

        [Fact]
        public async Task GetCommentsByUserId_UserId_ReturnsCommentsOfId()
        {
            //arrange
            List<Comment> comments = new List<Comment>
            {
                new Comment
                {
                    CommentId = 1,
                    UserId = 1,
                    PostId = 1,
                    Comment1 = "CommentText"
                }
            };

            _commentRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(comments);

            var controller = new CommentController(_commentRepositoryMock.Object);

            //act
            var result = await controller.GetCommentByUserId(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<Comment>>(actual);
            Assert.Equal(1, (actual as List<Comment>).Count);
        }
    }
}
