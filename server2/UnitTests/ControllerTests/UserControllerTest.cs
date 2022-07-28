//using CryptoHubAPI.Controllers;
//using BusinessLogic.Services.UserService;
//using BusinessLogic.Services.UserCoinService;
//using BusinessLogic.Services.SearchService;
//using Domain.IRepository;
//using Domain.Models;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Linq.Expressions;
//using Infrastructure.DTO.UserDTOs;
//using Infrastructure.DTO.UserCoinDTOs;
//using Infrastructure.DTO.ImageDTOs;

//namespace UnitTests.ControllerTests
//{
//    public class UserControllerTest
//    {

//        private readonly Mock<IUserService> _userServiceMock;
//        private readonly Mock<IUserCoinService> _userCoinServiceMock;
//        private readonly Mock<ISearchService> _searchServiceMock;
//        private readonly Mock<IUserRepository> _userRepositoryMock;

//        public UserControllerTest()
//        {
//            _userServiceMock = new Mock<IUserService>();
//            _userCoinServiceMock = new Mock<IUserCoinService>();
//            _searchServiceMock = new Mock<ISearchService>();
//            _userRepositoryMock = new Mock<IUserRepository>();
//        }


//        [Fact]
//        public async Task GetAllUsers_ListOfUsers_ReturnsListOfUsers()
//        {
//            //arrange
//            List<UserDTO> users = new List<UserDTO>
//            {
//                new UserDTO
//                {
//                    UserId = 1,
//                    //Email = "johndoe@gmail.com",
//                    Firstname = "john",
//                    Lastname = "doe",
//                    Username = "john"
//                },
//                new UserDTO
//                {
//                    UserId = 2,
//                    //Email = "elonmusk@gmail.com",
//                    Firstname = "elon",
//                    Lastname = "musk",
//                    Username = "elon"
//                },
//                new UserDTO
//                {
//                    UserId = 3,
//                    //Email = "billgates@gmail.com",
//                    Firstname = "bill",
//                    Lastname = "gates",
//                    Username = "bill"
//                }
//            };

//            _userServiceMock.Setup(u => u.GetAllUsers()).Returns(Task.FromResult(users));

//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.GetAllUsers();


//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);


//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<UserDTO>>(actual);
//            Assert.Equal(3, (actual as List<UserDTO>).Count);
//        }

//        [Fact]
//        public async Task GetUserById_UserId_ReturnsUserOfId()
//        {
//            //arrange
//            var user = new UserDTO
//            {
//                UserId = 1,
//                //Email = "johndoe@gmail.com",
//                Firstname = "john",
//                Lastname = "doe",
//                Username = "john"
//            };

//            _userServiceMock.Setup(u => u.GetById(user.UserId)).Returns(Task.FromResult(user));


//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.GetUserById(1);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<UserDTO>(actual);
//        }

//        //[Fact]
//        //public async Task GetUserByEmail_UserEmail_ReturnsUserOfEmail()
//        //{
//        //    //arrange
//        //    var user = new UserDTO
//        //    {
//        //        UserId = 1,
//        //        //Email = "johndoe@gmail.com",
//        //        Firstname = "john",
//        //        Lastname = "doe",
//        //        Username = "john"
//        //    };

//        //    _userServiceMock.Setup(u => u.GetUserByEmail(user.Email)).Returns(Task.FromResult(user));

//        //    var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//        //    //act
//        //    var result = await controller.GetUserByEmail(user.Email);

//        //    //assert
//        //    Assert.NotNull(result);
//        //    Assert.IsType<OkObjectResult>(result.Result);

//        //    var actual = (result.Result as OkObjectResult).Value;
//        //    Assert.IsType<UserDTO>(actual);

//        //    //arrange 2
//        //    _userServiceMock.Setup(u => u.GetUserByEmail(user.Email)).Returns((Task<UserDTO>)null);

//        //    //act 2
//        //    var result2 = await controller.GetUserByEmail("MadeUpEmail");

//        //    //assert 2
//        //    Assert.NotNull(result2);
//        //    Assert.IsType<ActionResult<UserDTO>>(result2);

//        //    var actual2 = (result.Result as NotFoundObjectResult);
//        //    Assert.Null(actual2);
//        //}

//        [Fact]
//        public async Task AddUser_User_ReturnsUser()
//        {
//            //arrange
//            var user = new User
//            {
//                UserId = 1,
//                Email = "johndoe@gmail.com",
//                Firstname = "john",
//                Lastname = "doe",
//                Username = "john",
//                Password = "1234"
//            };
//            var userDTO = new UserDTO
//            {
//                UserId = 1,
//                //Email = "johndoe@gmail.com",
//                Firstname = "john",
//                Lastname = "doe",
//                Username = "john"
//            };
//            _userServiceMock.Setup(u => u.AddUser(user)).Returns(Task.FromResult(userDTO));

//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.AddUser(user);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<UserDTO>(actual);

//            ////arrange 2
//            //_userServiceMock.Setup(u => u.AddUser(user)).Returns((Task<UserDTO>)null);
//            ////_userRepositoryMock.Setup(u => u.Add(user)).Returns((Task<User>)null);

//            ////act 2
//            //var result2 = await controller.AddUser(user);

//            ////assert 2
//            //Assert.Null(result2);
//            //Assert.IsType<ActionResult<UserDTO>>(result2);

//            //var actual2 = (result.Result as NotFoundObjectResult);
//            //Assert.Null(actual2);
//        }

//        [Fact]
//        public async Task UpdateUser_User_ReturnsUser()
//        {
//            //arrange
//            var user = new User
//            {
//                UserId = 1,
//                Email = "johndoe@gmail.com",
//                Firstname = "john",
//                Lastname = "doe",
//                Username = "john",
//                Password = "1234"
//            };
//            var userDTO = new UserDTO
//            {
//                UserId = 1,
//                //Email = "johndoe@gmail.com",
//                Firstname = "john",
//                Lastname = "doe",
//                Username = "john"
//            };

//            _userServiceMock.Setup(u => u.UpateUser(user)).Returns(Task.FromResult(userDTO));

//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.UpdateUser(user);

//            Assert.NotNull(result);
//            Assert.IsType<OkObjectResult>(result.Result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<UserDTO>(actual);
//        }

//        [Fact]
//        public async Task Delete_User_None()
//        {
//            //arrange
//            var user = new User
//            {
//                UserId = 1,
//                Email = "johndoe@gmail.com",
//                Firstname = "john",
//                Lastname = "doe",
//                Username = "john",
//                Password = "1234"
//            };

//            _userServiceMock.Setup(u => u.Delete(user.UserId)).Returns(Task.FromResult(user));

//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.Delete(user.UserId);

//            Assert.NotNull(result);
//            Assert.IsType<OkResult>(result);
//        }

//        [Fact]
//        public async Task GetAllUsersFollowingCoin_CoinId_None()
//        {
//            //arrange
//            var userCoin = new List<UserCoinDTO>
//            {
//                new UserCoinDTO
//                {
//                    UserId = 1,
//                    CoinId = 1
//                },
//                new UserCoinDTO
//                {
//                    UserId = 2,
//                    CoinId = 1
//                }
//            };

//            _userCoinServiceMock.Setup(u => u.GetUserCoins(1)).Returns(Task.FromResult(userCoin));

//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.GetAllUsersFollowingCoin(1);

//            //assert
//            Assert.NotNull(result);
//            Assert.IsType<ActionResult<List<UserCoinDTO>>>(result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<UserCoinDTO>>(actual);

//            //arrange 2
//            _userCoinServiceMock.Setup(u => u.GetUserCoins(1)).Returns((Task<List<UserCoinDTO>>)null);

//            //act 2
//            var result2 = await controller.GetAllUsersFollowingCoin(9001);

//            //assert 2
//            Assert.NotNull(result2);
//            Assert.IsType<ActionResult<List<UserCoinDTO>>>(result2);

//            var actual2 = (result.Result as NotFoundObjectResult);
//            Assert.Null(actual2);
//        }

//        [Fact]
//        public async Task SearchUser_userIdsearchTerm_ListOfUsers()
//        {
//            //arrange
//            var users = new List<SearchDTO>
//            {
//                new SearchDTO
//                {
//                    UserId = 1,
//                    Firstname = "John",
//                    Lastname = "Snow",
//                    Username = "JS"
//                },
//                new SearchDTO
//                {
//                    UserId = 2,
//                    Firstname = "Jane",
//                    Lastname = "Lava",
//                    Username = "JL"
//                },
//                new SearchDTO
//                {
//                    UserId = 3,
//                    Firstname = "James",
//                    Lastname = "Brown",
//                    Username = "JB"
//                }
//            };

//            _searchServiceMock.Setup(u => u.SearchUser(1, "J")).Returns(Task.FromResult(users));

//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.SearchUser(1, "J");

//            //assert
//            Assert.NotNull(result);
//            Assert.IsType<ActionResult<List<User>>>(result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<SearchDTO>>(actual);

//            //arrange 2
//            _searchServiceMock.Setup(u => u.SearchUser(1, "J")).Returns((Task<List<SearchDTO>>)null);

//            //act 2
//            var result2 = await controller.SearchUser(1, "MadeUpName");

//            //assert 2
//            Assert.NotNull(result2);
//            Assert.IsType<ActionResult<List<User>>>(result2);

//            var actual2 = (result.Result as NotFoundObjectResult);
//            Assert.Null(actual2);
//        }

//        [Fact]
//        public async Task SuggestedUsers_userId_ListOfUsers()
//        {
//            //arrange
//            var users = new List<SearchDTO>
//            {
//                new SearchDTO
//                {
//                    UserId = 1,
//                    Firstname = "John",
//                    Lastname = "Snow",
//                    Username = "JS"
//                },
//                new SearchDTO
//                {
//                    UserId = 2,
//                    Firstname = "Jane",
//                    Lastname = "Lava",
//                    Username = "JL"
//                },
//                new SearchDTO
//                {
//                    UserId = 3,
//                    Firstname = "James",
//                    Lastname = "Brown",
//                    Username = "JB"
//                }
//            };

//            _userServiceMock.Setup(u => u.SuggestedUsers(1)).Returns(Task.FromResult(users));

//            var controller = new UserController(_userServiceMock.Object, _userCoinServiceMock.Object, _searchServiceMock.Object);

//            //act
//            var result = await controller.SuggestedUsers(1);

//            //assert
//            Assert.NotNull(result);
//            Assert.IsType<ActionResult<List<User>>>(result);

//            var actual = (result.Result as OkObjectResult).Value;
//            Assert.IsType<List<SearchDTO>>(actual);

//            //arrange 2
//            _userServiceMock.Setup(u => u.SuggestedUsers(1)).Returns((Task<List<SearchDTO>>)null);

//            //act 2
//            var result2 = await controller.SuggestedUsers(0);

//            //assert 2
//            Assert.NotNull(result2);
//            Assert.IsType<ActionResult<List<User>>>(result2);

//            var actual2 = (result.Result as NotFoundObjectResult);
//            Assert.Null(actual2);
//        }

//        //[Fact]
//        //public async Task UpdateProfileImage_CreateImageDTO_ActionResult()
//        //{
//        //    //arrange
//        //    var imageDTO = new CreateImageDTO
//        //    {
//        //        Name = "sample",
//        //        Blob = "100110 111010 001011 101001"
//        //    };

//        //    _userServiceMock.Setup(u => u.UploadProfilePic(imageDTO)).Returns(Task.FromResult(imageDTO));
//        //}
//    }
//}