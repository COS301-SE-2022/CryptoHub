﻿//using BusinessLogic.Services.ReplyService;
//using BusinessLogic.Services.UserService;
//using BusinessLogic.Services.CommentService;
//using AutoMapper;
//using CryptoHubAPI.Controllers;
//using Domain.IRepository;
//using Domain.Models;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Linq.Expressions;
//using Infrastructure.DTO.ReplyDTOs;

//namespace UnitTests.ControllerTests
//{
//    public class ReplyControllerTest
//    {
//        private readonly Mock<IReplyService> _replyServiceMock;
//        private readonly Mock<IUserService> _userServiceMock;
//        private readonly Mock<ICommentService> _commentServiceMock;
//        private readonly Mock<IMapper> _mapper;

//        public ReplyControllerTest()
//        {
//            _replyServiceMock = new Mock<IReplyService>();
//            _userServiceMock = new Mock<IUserService>();
//            _commentServiceMock = new Mock<ICommentService>();
//            _mapper = new Mock<IMapper>();
//        }

//        [Fact]
//        public async Task GetRepliesByUserId_UserId_ReturnsRepliesOfId()
//        {
//            //arrange
//            List<Reply> replies = new List<Reply>
//            {
//                new Reply
//                {
//                    ReplyId = 1,
//                    UserId = 1,
//                    CommentId = 1,
//                    Content = "ReplyText"
//                }
//            };
//            var user = new User
//            {
//                UserId = 1,
//                Email = "johndoe@gmail.com",
//                Firstname = "john",
//                Lastname = "doe",
//                Username = "john",
//                Password = "1234"
//            };

//            _replyServiceMock.Setup(u => u.GetRepliesByUserId(user.UserId));
//            _userServiceMock.Setup(u => u.GetById(user.UserId));


//            var controller = new ReplyController(_replyRepositoryMock.Object, _commentRepositoryMock.Object, _userRepositoryMock.Object, _mapper.Object);

//            //act
//            var result = await controller.GetRepliesByUserId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<Reply>>(actual);
//            Assert.Equal(1, (actual as List<Reply>).Count);
//        }

//        [Fact]
//        public async Task GetRepliesByCommentId_CommentId_ReturnsRepliesOfId()
//        {
//            //arrange
//            List<Reply> replies = new List<Reply>
//            {
//                new Reply
//                {
//                    ReplyId = 1,
//                    UserId = 1,
//                    CommentId = 1,
//                    Comment = "ReplyText"
//                }
//            };
//            var comment = new Comment
//            {
//                CommentId = 1,
//                UserId = 1,
//                PostId = 1,
//                Comment1 = "CommentText"
//            };

//            _replyRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<Reply, bool>>>())).ReturnsAsync(replies);
//            _commentRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(comment);


//            var controller = new ReplyController(_replyRepositoryMock.Object, _commentRepositoryMock.Object, _userRepositoryMock.Object);

//            //act
//            var result = await controller.GetRepliesByCommentId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<Reply>>(actual);
//            Assert.Equal(1, (actual as List<Reply>).Count);
//        }

//        [Fact]
//        public async Task GetRepliesCountByCommentId_CommentId_ReturnsCountOfReplies()
//        {
//            //arrange
//            List<Reply> replies = new List<Reply>
//            {
//                new Reply
//                {
//                    ReplyId = 1,
//                    UserId = 1,
//                    CommentId = 1,
//                    Comment = "ReplyText"
//                },
//                new Reply
//                {
//                    ReplyId = 2,
//                    UserId = 2,
//                    CommentId = 1,
//                    Comment = "ReplyText2"
//                }
//            };
//            var comment = new Comment
//            {
//                CommentId = 1,
//                UserId = 1,
//                PostId = 1,
//                Comment1 = "CommentText"
//            };

//            _replyRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<Reply, bool>>>())).ReturnsAsync(replies);
//            _commentRepositoryMock.Setup(u => u.FindOne(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(comment);


//            var controller = new ReplyController(_replyRepositoryMock.Object, _commentRepositoryMock.Object, _userRepositoryMock.Object);


//            //act
//            var result = await controller.GetRepliesCountByCommentId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result);

//            var actual = (result as OkObjectResult).Value;

//            var x = actual.GetType().GetProperty("Count").GetValue(actual, null);

//            Assert.Equal(2, x);

//        }

//        [Fact]
//        public async Task AddReply_Reply_ReturnsReply()
//        {
//            //arrange
//            var reply = new Reply
//            {
//                ReplyId = 1,
//                UserId = 1,
//                CommentId = 1,
//                Comment = "ReplyText"
//            };
//            _replyRepositoryMock.Setup(u => u.Add(It.IsAny<Reply>())).ReturnsAsync(reply);

//            var controller = new ReplyController(_replyRepositoryMock.Object, _commentRepositoryMock.Object, _userRepositoryMock.Object);

//            //act
//            var result = await controller.AddReply(reply);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<Reply>(actual);
//        }

//        [Fact]
//        public async Task UpdateReply_Reply_ReturnsReply()
//        {
//            //arrange
//            var reply = new Reply
//            {
//                ReplyId = 1,
//                UserId = 1,
//                CommentId = 1,
//                Comment = "ReplyText"
//            };

//            _replyRepositoryMock.Setup(u => u.Update(It.IsAny<Expression<Func<Reply, bool>>>(), It.IsAny<Reply>())).ReturnsAsync(reply);

//            var controller = new ReplyController(_replyRepositoryMock.Object, _commentRepositoryMock.Object, _userRepositoryMock.Object);

//            //act
//            var result = await controller.UpdateReply(reply.ReplyId, reply);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result);

//            var actual = (result as OkObjectResult).Value;
//            Assert.IsType<Reply>(actual);
//        }

//        [Fact]
//        public async Task Delete_Reply_ReturnsNone()
//        {
//            //arrange
//            var reply = new Reply
//            {
//                ReplyId = 1,
//                UserId = 1,
//                CommentId = 1,
//                Comment = "ReplyText"
//            };

//            _replyRepositoryMock.Setup(u => u.DeleteOne(It.IsAny<Expression<Func<Reply, bool>>>()));

//            var controller = new ReplyController(_replyRepositoryMock.Object, _commentRepositoryMock.Object, _userRepositoryMock.Object);

//            //act
//            var result = await controller.Delete(reply.ReplyId);

//            Assert.NotNull(result);
//            Assert.IsType<OkResult>(result);
//        }
//    }
//}
