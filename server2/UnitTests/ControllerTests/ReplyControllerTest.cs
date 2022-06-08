using CryptoHubAPI.Controllers;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.ControllerTests
{
    public class ReplyControllerTest
    {
        private readonly Mock<IReplyRepository> _replyRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ICommentRepository> _commentRepositoryMock;

        public ReplyControllerTest()
        {
            _replyRepositoryMock = new Mock<IReplyRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _commentRepositoryMock = new Mock<ICommentRepository>();
        }

        [Fact]
        public async Task GetRepliesByReplyId_ReplyId_ReturnsRepliesOfId()
        {
            //arrange
            List<Reply> replies = new List<Reply>
            {
                new Reply
                {
                    ReplyId = 1,
                    UserId = 1,
                    CommentId = 1,
                    Comment = "ReplyText"
                }
            };
            var user = new User
            {
                UserId = 1,
                Email = "johndoe@gmail.com",
                Firstname = "john",
                Lastname = "doe",
                Username = "john",
                Password = "1234"
            };

            _replyRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<Reply, bool>>>())).ReturnsAsync(replies);
            _userRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);


            var controller = new ReplyController(_replyRepositoryMock.Object, _commentRepositoryMock.Object, _userRepositoryMock.Object);

            //act
            var result = await controller.GetRepliesByUserId(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var actual = (result.Result as OkObjectResult).Value;
            Assert.IsType<List<Reply>>(actual);
            Assert.Equal(1, (actual as List<Reply>).Count);
        }
    }
}
