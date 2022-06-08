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
        public async Task GetCommentsByCommentId_CommentId_ReturnsCommentsOfId()
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

        [Fact]
        public async Task GetCommentsByPostId_PostId_ReturnsCommentsOfId()
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

        [Fact]
        public async Task AddComment_Comment_ReturnsComment()
        {
            //arrange
            var comment = new Comment
            {
                CommentId = 1,
                UserId = 1,
                PostId = 1,
                Comment1 = "CommentText"
            };
            _commentRepositoryMock.Setup(u => u.Add(It.IsAny<Comment>())).ReturnsAsync(comment);

            var controller = new CommentController(_commentRepositoryMock.Object);

            //act
            var result = await controller.AddComment(comment);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Comment>(actual);
        }

        [Fact]
        public async Task UpdateComment_Comment_ReturnsComment()
        {
            //arrange
            var comment = new Comment
            {
                CommentId = 1,
                UserId = 1,
                PostId = 1,
                Comment1 = "CommentText"
            };

            _commentRepositoryMock.Setup(u => u.Update(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<Comment>())).ReturnsAsync(comment);

            var controller = new CommentController(_commentRepositoryMock.Object);

            //act
            var result = await controller.UpdateComment(comment);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<Comment>(actual);
        }

        [Fact]
        public async Task Delete_Comment_ReturnsNone()
        {
            //arrange
            var comment = new Comment
            {
                CommentId = 1,
                UserId = 1,
                PostId = 1,
                Comment1 = "CommentText"
            };

            _commentRepositoryMock.Setup(u => u.DeleteOne(It.IsAny<Expression<Func<Comment, bool>>>()));

            var controller = new CommentController(_commentRepositoryMock.Object);

            //act
            var result = await controller.Delete(comment.CommentId);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
