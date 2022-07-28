//using BusinessLogic.Services.CommentService;
//using CryptoHubAPI.Controllers;
//using Domain.IRepository;
//using Domain.Models;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Linq.Expressions;
//using Infrastructure.DTO.CommentDTOs;

//namespace UnitTests.ControllerTests
//{
//    public class CommentControllerTest
//    {
//        private readonly Mock<ICommentService> _commentServiceMock;

//        public CommentControllerTest()
//        {
//            _commentServiceMock = new Mock<ICommentService>();
//        }

//        [Fact]
//        public async Task GetCommentsByUserId_CommentId_ReturnsCommentsOfId()
//        {
//            //arrange
//            List<CommentDTO> comments = new List<CommentDTO>
//            {
//                new CommentDTO
//                {
//                    CommentId = 1,
//                    UserId = 1,
//                    PostId = 1,
//                    Content = "sample comment"
//                },
//                new CommentDTO
//                {
//                    CommentId = 2,
//                    UserId = 2,
//                    PostId = 2,
//                    Content = "sample comment"
//                }
//            };

//            _commentServiceMock.Setup(u => u.GetCommentByUserId(1));

//            var controller = new CommentController(_commentServiceMock.Object);

//            //act
//            var result = await controller.GetCommentByUserId(1);

//            //assert
//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<Comment>>(actual);
//            Assert.Equal(1, (actual as List<Comment>).Count);

//        }

//        [Fact]
//        public async Task GetCommentsByPostId_PostId_ReturnsCommentsOfId()
//        {
//            //arrange
//            List<CommentDTO> comments = new List<CommentDTO>
//            {
//                new CommentDTO
//                {
//                    CommentId = 1,
//                    UserId = 1,
//                    PostId = 1,
//                    Content = "sample comment"
//                },
//                new CommentDTO
//                {
//                    CommentId = 2,
//                    UserId = 2,
//                    PostId = 2,
//                    Content = "sample comment"
//                }
//            };

//            _commentServiceMock.Setup(u => u.GetCommentByPostId(1));

//            var controller = new CommentController(_commentServiceMock.Object);

//            //act
//            var result = await controller.GetCommentByPostId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<Comment>>(actual);
//            Assert.Equal(1, (actual as List<Comment>).Count);
//        }

//        [Fact]
//        public async Task GetCommentCountByPostId_PostId_ReturnsCountOfComments()
//        {
//            //arrange
//            List<CommentDTO> comments = new List<CommentDTO>
//            {
//                new CommentDTO
//                {
//                    CommentId = 1,
//                    UserId = 1,
//                    PostId = 1,
//                    Content = "sample comment"
//                },
//                new CommentDTO
//                {
//                    CommentId = 2,
//                    UserId = 2,
//                    PostId = 2,
//                    Content = "sample comment"
//                }
//            };

//            _commentServiceMock.Setup(u => u.GetCommentCountByPostId(1));

//            var controller = new CommentController(_commentServiceMock.Object);

//            //act
//            var result = await controller.GetCommentCountByPostId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;

//            var x = actual.GetType().GetProperty("Count").GetValue(actual, null);

//            Assert.Equal(2, x);

//        }

//        [Fact]
//        public async Task AddComment_Comment_ReturnsComment()
//        {
//            //arrange
//            var comment = new CommentDTO
//            {
//                CommentId = 1,
//                UserId = 1,
//                PostId = 1,
//                Content = "sample comment"
//            };
//            _commentServiceMock.Setup(u => u.AddComment(comment));

//            var controller = new CommentController(_commentServiceMock.Object);

//            //act
//            var result = await controller.AddComment(comment);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<Comment>(actual);
//        }

//        [Fact]
//        public async Task UpdateComment_Comment_ReturnsComment()
//        {
//            //arrange
//            var comment = new Comment
//            {
//                CommentId = 1,
//                UserId = 1,
//                PostId = 1,
//                Content = "sample comment"
//            };

//            _commentServiceMock.Setup(u => u.UpdateComment(comment));

//            var controller = new CommentController(_commentServiceMock.Object);

//            //act
//            var result = await controller.UpdateComment(comment);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<Comment>(actual);
//        }

//        [Fact]
//        public async Task Delete_Comment_ReturnsNone()
//        {
//            //arrange
//            var comment = new Comment
//            {
//                CommentId = 1,
//                UserId = 1,
//                PostId = 1,
//                Content = "sample comment"
//            };

//            _commentServiceMock.Setup(u => u.Delete(comment.CommentId));

//            var controller = new CommentController(_commentServiceMock.Object);

//            //act
//            var result = await controller.Delete(comment.CommentId);

//            Assert.NotNull(result);
//            Assert.IsType<OkResult>(result);
//        }
//    }
//}
