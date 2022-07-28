//using BusinessLogic.Services.LikeService;
//using CryptoHubAPI.Controllers;
//using Domain.IRepository;
//using Domain.Models;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Linq.Expressions;
//using Infrastructure.DTO.LikeDTOs;

//namespace UnitTests.ControllerTests
//{
//    public class LikeControllerTest
//    {
//        private readonly Mock<ILikeService> _likeServiceMock;

//        public LikeControllerTest()
//        {
//            _likeServiceMock = new Mock<ILikeService>();
//        }

//        //[Fact]
//        //public async Task GetAllLikes_ListOfLikes_ReturnsListOfLikes()
//        //{
//        //    //arrange
//        //    List<Like> likes = new List<Like>
//        //    {
//        //        new Like
//        //        {
//        //            LikeId = 1,
//        //            UserId = 1,
//        //            PostId = 1,
//        //        },
//        //        new Like
//        //        {
//        //            LikeId = 2,
//        //            UserId = 2,
//        //            PostId = 2,
//        //        },
//        //        new Like
//        //        {
//        //            LikeId = 3,
//        //            UserId = 3,
//        //            PostId = 3,
//        //        }
//        //    };

//        //    _likeServiceMock.Setup(u => u.GetAllLikes());

//        //    var controller = new LikeController(_likeServiceMock.Object);

//        //    //act
//        //    var result = await controller.GetAllLikes();


//        //    Assert.NotNull(result);
//        //    Assert.IsType<OkObjectResult>(result.Result);


//        //    var actual = (result.Result as OkObjectResult).Value;
//        //    Assert.IsType<List<Like>>(actual);
//        //    Assert.Equal(3, (actual as List<Like>).Count);
//        //}

//        //[Fact]
//        //public async Task GetLikesByUserId_UserId_ReturnsLikesOfId()
//        //{
//        //    //arrange
//        //    List<Like> likes = new List<Like>
//        //    {
//        //        new Like
//        //        {
//        //            LikeId = 1,
//        //            UserId = 1,
//        //            PostId = 1,
//        //        }
//        //    };

//        //    _likeServiceMock.Setup(u => u.GetLikeByPostId(1));

//        //    var controller = new LikeController(_likeServiceMock.Object);

//        //    //act
//        //    var result = await controller.GetLikeByUserId(1);

//        //    Assert.NotNull(result);
//        //    Assert.IsType<OkObjectResult>(result.Result);

//        //    var actual = (result.Result as OkObjectResult).Value;
//        //    Assert.IsType<List<Like>>(actual);
//        //    Assert.Equal(1, (actual as List<Like>).Count);

//        //    //arrange 2
//        //    _likeRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<Like, bool>>>())).ReturnsAsync((List<Like>)null);

//        //    //act
//        //    var result2 = await controller.GetLikeByUserId(1);

//        //    Assert.NotNull(result2);
//        //    Assert.IsType<NotFoundResult>(result2.Result);

//        //}

//        [Fact]
//        public async Task GetLikesByPostId_PostId_ReturnsLikesOfId()
//        {
//            //arrange
//            List<Like> likes = new List<Like>
//            {
//                new Like
//                {
//                    LikeId = 1,
//                    UserId = 1,
//                    PostId = 1,
//                }
//            };

//            _likeServiceMock.Setup(u => u.GetLikeByPostId(1));

//            var controller = new LikeController(_likeServiceMock.Object);

//            //act
//            var result = await controller.GetLikeByPostId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<Like>>(actual);
//            Assert.Equal(1, (actual as List<Like>).Count);

//            //arrange 2
//            _likeServiceMock.Setup(u => u.GetLikeByPostId(1)).Returns((Task<List<LikeDTO>>)null);

//            //act
//            var result2 = await controller.GetLikeByPostId(1);

//            Assert.NotNull(result2);
//            Assert.IsType<NotFoundResult>(result2.Result);
//        }

//        [Fact]
//        public async Task GetLikeCountByPostId_PostId_ReturnsCountOfLikes()
//        {
//            //arrange
//            List<LikeDTO> likes = new List<LikeDTO>
//            {
//                new LikeDTO
//                {
//                    LikeId = 1,
//                    UserId = 1,
//                    PostId = 1,
//                },
//                new LikeDTO
//                {
//                    LikeId = 2,
//                    UserId = 2,
//                    PostId = 1,
//                },
//                new LikeDTO
//                {
//                    LikeId = 3,
//                    UserId = 3,
//                    PostId = 1,
//                }
//            };

//            _likeServiceMock.Setup(u => u.GetLikeByPostId(1));

//            var controller = new LikeController(_likeServiceMock.Object);

//            //act
//            var result = await controller.GetLikeCountByPostId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result);

//            var actual = (result as OkObjectResult).Value;

//            var x = actual.GetType().GetProperty("Count").GetValue(actual, null);

//            Assert.Equal(3, x);

//            //arrange 2
//            _likeServiceMock.Setup(u => u.GetLikeCountByPostId(1)).Returns((Task<Response<object>>)null);

//            //act
//            var result2 = await controller.GetLikeCountByPostId(1);

//            Assert.NotNull(result2);
//            Assert.IsType<NotFoundResult>(result2);
//        }

//        [Fact]
//        public async Task GetLikeByCommentId_CommentId_ReturnsLikesOfId()
//        {
//            //arrange
//            List<Like> likes = new List<Like>
//            {
//                new Like
//                {
//                    LikeId = 1,
//                    UserId = 1,
//                    PostId = 1,
//                    CommentId = 1,
//                }
//            };

//            _likeServiceMock.Setup(u => u.GetLikeByCommentId(1));

//            var controller = new LikeController(_likeServiceMock.Object);

//            //act
//            var result = await controller.GetLikeByCommentId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<Like>>(actual);
//            Assert.Equal(1, (actual as List<Like>).Count);
//        }

//        [Fact]
//        public async Task GetLikeCountByCommentId_CommentId_ReturnsCountOfLikes()
//        {
//            //arrange
//            List<Like> likes = new List<Like>
//            {
//                new Like
//                {
//                    LikeId = 1,
//                    UserId = 1,
//                    PostId = 1,
//                    CommentId = 1,
//                },
//                new Like
//                {
//                    LikeId = 2,
//                    UserId = 2,
//                    PostId = 1,
//                    CommentId = 1,
//                },
//                new Like
//                {
//                    LikeId = 3,
//                    UserId = 3,
//                    PostId = 1,
//                    CommentId = 1,
//                }
//            };

//            _likeServiceMock.Setup(u => u.GetLikeCountByCommentId(1));

//            var controller = new LikeController(_likeServiceMock.Object);

//            //act
//            var result = await controller.GetLikeCountByCommentId(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result);

//            var actual = (result as OkObjectResult).Value;

//            var x = actual.GetType().GetProperty("Count").GetValue(actual, null);

//            Assert.Equal(3, x);

//        }

//        //[Fact]
//        //public async Task GetLikeByReplyId_PostId_ReturnsLikesOfId()
//        //{
//        //    //arrange
//        //    List<Like> likes = new List<Like>
//        //    {
//        //        new Like
//        //        {
//        //            LikeId = 1,
//        //            UserId = 1,
//        //            PostId = 1,
//        //            ReplyId = 1,
//        //        }
//        //    };

//        //    _likeServiceMock.Setup(u => u.GetLikeByReplyId(1));

//        //    var controller = new LikeController(_likeServiceMock.Object);

//        //    //act
//        //    var result = await controller.GetLikeByReplyId(1);

//        //    Assert.NotNull(result);
//        //    Assert.IsType<OkObjectResult>(result.Result);

//        //    var actual = (result.Result as OkObjectResult).Value;
//        //    Assert.IsType<List<Like>>(actual);
//        //    Assert.Equal(1, (actual as List<Like>).Count);
//        //}

//        //[Fact]
//        //public async Task GetLikeCountByReplyId_ReplyId_ReturnsCountOfLikes()
//        //{
//        //    //arrange
//        //    List<Like> likes = new List<Like>
//        //    {
//        //        new Like
//        //        {
//        //            LikeId = 1,
//        //            UserId = 1,
//        //            PostId = 1,
//        //            CommentId = 1,
//        //        },
//        //        new Like
//        //        {
//        //            LikeId = 2,
//        //            UserId = 2,
//        //            PostId = 1,
//        //            CommentId = 1,
//        //        },
//        //        new Like
//        //        {
//        //            LikeId = 3,
//        //            UserId = 3,
//        //            PostId = 1,
//        //            CommentId = 1,
//        //        }
//        //    };

//        //    _likeRepositoryMock.Setup(u => u.FindRange(It.IsAny<Expression<Func<Like, bool>>>())).ReturnsAsync(likes);

//        //    var controller = new LikeController(_likeRepositoryMock.Object);

//        //    //act
//        //    var result = await controller.GetLikeCountByReplyId(1);

//        //    Assert.NotNull(result);
//        //    Assert.IsType<OkObjectResult>(result);

//        //    var actual = (result as OkObjectResult).Value;

//        //    var x = actual.GetType().GetProperty("Count").GetValue(actual, null);

//        //    Assert.Equal(3, x);

//        //}

//        [Fact]
//        public async Task AddLike_Like_ReturnsLike()
//        {
//            //arrange
//            var like = new LikeDTO
//            {
//                LikeId = 1,
//                UserId = 1,
//                PostId = 1,
//            };

//            _likeServiceMock.Setup(u => u.AddLike(like));

//            var controller = new LikeController(_likeServiceMock.Object);

//            //act
//            var result = await controller.AddLike(like);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<Like>(actual);
//        }

//        [Fact]
//        public async Task UpdateLike_Like_ReturnsLike()
//        {
//            //arrange
//            var likeDTO = new LikeDTO
//            {
//                LikeId = 1,
//                UserId = 1,
//                PostId = 1,
//            };

//            var like = new Like
//            {
//                LikeId = 1,
//                UserId = 1,
//                PostId = 1,
//            };

//            _likeServiceMock.Setup(u => u.AddLike(likeDTO));

//            var controller = new LikeController(_likeServiceMock.Object);

//            //act
//            var result = await controller.UpdateLike(like);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<Like>(actual);
//        }

//        [Fact]
//        public async Task Delete_Like_None()
//        {
//            //arrange
//            var like = new Like
//            {
//                LikeId = 1,
//                UserId = 1,
//                PostId = 1,
//            };

//            _likeServiceMock.Setup(u => u.Delete(like.UserId, like.PostId!.Value));

//            var controller = new LikeController(_likeServiceMock.Object);

//            //act
//            var result = await controller.Delete(like.UserId, like.PostId!.Value);

//            Assert.NotNull(result);
//            Assert.IsType<OkResult>(result);
//        }
//    }
//}
