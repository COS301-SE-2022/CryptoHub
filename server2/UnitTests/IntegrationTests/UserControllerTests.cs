//using Domain.Models;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Microsoft.EntityFrameworkCore;
////using Domain.Infrastructure;
//using Microsoft.AspNetCore.Mvc;
//using Infrastructure.Data;

//namespace UnitTests.IntegrationTests
//{
//    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly HttpClient _httpClient;
//        //private CryptoHubDBContext _context;


//        public UserControllerTests()
//        {
//            var dbname = Guid.NewGuid().ToString();
//            var appFactory = new WebApplicationFactory<Program>()
//                             .WithWebHostBuilder(builder =>
//                             {
//                                 builder.ConfigureServices(
//                                     services =>
//                                     {
//                                         var descriptor = services.SingleOrDefault(
//                                             d => d.ServiceType == typeof(DbContextOptions<CryptoHubDBContext>));

//                                         if (descriptor != null)
//                                         {
//                                             services.Remove(descriptor);
//                                         }
//                                         services.AddDbContext<CryptoHubDBContext>(
//                                             options =>
//                                             {
//                                                 options.UseInMemoryDatabase(dbname);
//                                             });
//                                     });
//                             });

//            _httpClient = appFactory.CreateClient();
//        }

//        [Fact]
//        public async Task GetAllUsers_NoUsers()
//        {
//            //Act
//            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

//            //Assert
//            Assert.NotNull(response);
//            Assert.Equal(200, (double)response.StatusCode);

//            var users = await response.Content.ReadAsAsync<List<User>>();

//            Assert.Equal(0, users.Count());
//        }

//        [Fact]
//        public async Task GetAllUsers_Users()
//        {
//            //Arrange
//            var testUser = new User()
//            {

//                Firstname = "test1",
//                Lastname = "user1",
//                Username = "user1",
//                Email = "test@gmail.com",
//                Password = "1234"

//            };
//            var testUser2 = new User()
//            {

//                Firstname = "test2",
//                Lastname = "user2",
//                Username = "user2",
//                Email = "test2@gmail.com",
//                Password = "1234"

//            };
//            var testUser3 = new User()
//            {

//                Firstname = "test3",
//                Lastname = "user3",
//                Username = "user3",
//                Email = "test3@gmail.com",
//                Password = "1234"

//            };

//            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
//            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
//            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

//            //Act
//            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

//            //Assert
//            Assert.NotNull(response);
//            Assert.Equal(200, (double)response.StatusCode);

//            var users = await response.Content.ReadAsAsync<List<User>>();

//            Assert.Equal(3, users.Count);
//        }

//        [Fact]
//        public async Task GetUserById_NoUsers()
//        {
//            //Act
//            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserById/1");

//            //Assert
//            Assert.NotNull(response);
//            Assert.Equal(404, (double)response.StatusCode);
//        }

//        [Fact]
//        public async Task GetUserById_UserFound()
//        {
//            //Arrange
//            var testUser = new User()
//            {
//                //UserId = 1,
//                Firstname = "test",
//                Lastname = "user",
//                Username = "user1",
//                Email = "test@gmail.com",
//                Password = "1234",
//                RoleId = 3,
//            };
//            var testUser2 = new User()
//            {
//                //UserId = 2,
//                Firstname = "test",
//                Lastname = "user",
//                Username = "user2",
//                Email = "test2@gmail.com",
//                Password = "1234",
//                RoleId = 3,
//            };
//            var testUser3 = new User()
//            {
//                //UserId = 3,
//                Firstname = "test",
//                Lastname = "user",
//                Username = "user3",
//                Email = "test3@gmail.com",
//                Password = "1234",
//                RoleId = 3,
//            };

//            var x = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
//            var y = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
//            var z = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

//            //Act
//            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserById/1");

//            //Assert
//            Assert.NotNull(response);
//            Assert.Equal(200, (double)response.StatusCode);

//            var user = await response.Content.ReadAsAsync<User>();

//            Assert.NotNull(user);
//            Assert.Equal(testUser.UserId, user.UserId);
//            Assert.Equal(testUser.Firstname, user.Firstname);
//            Assert.Equal(testUser.Lastname, user.Lastname);
//            Assert.Equal(testUser.Username, user.Username);
//            Assert.Equal(testUser.Password, user.Password);
//            Assert.Equal(testUser.Email, user.Email);
//        }

//        [Fact]
//        public async Task GetUserById_UserNotFound()
//        {
//            //Arrange
//            var testUser = new User()
//            {
//                UserId = 1,
//                Firstname = "test",
//                Lastname = "user",
//                Username = "test user",
//                Email = "test@gmail.com",
//                Password = "1234"
//            };
//            var testUser2 = new User()
//            {
//                UserId = 2,
//                Firstname = "test",
//                Lastname = "user",
//                Username = "test user",
//                Email = "test2@gmail.com",
//                Password = "1234"
//            };
//            var testUser3 = new User()
//            {
//                UserId = 3,
//                Firstname = "test",
//                Lastname = "user",
//                Username = "test user",
//                Email = "test3@gmail.com",
//                Password = "1234"
//            };

//            var x = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
//            var y = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
//            var z = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

//            //Act
//            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserById/4");

//            //Assert
//            Assert.NotNull(response);
//            Assert.Equal(404, (double)response.StatusCode);
//        }

//        [Fact]
//        public async Task AddUser()
//        {
//            var testUser = new User()
//            {
//                UserId = 1,
//                Firstname = "test",
//                Lastname = "user",
//                Username = "test user",
//                Email = "test@gmail.com",
//                Password = "1234"

//            };

//            //Act
//            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
//            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserById/1");

//            //Assert
//            Assert.NotNull(response);
//            Assert.Equal(200, (double)response.StatusCode);

//            var user = await response.Content.ReadAsAsync<User>();

//            Assert.NotNull(user);
//            Assert.Equal(testUser.UserId, user.UserId);
//            Assert.Equal(testUser.Firstname, user.Firstname);
//            Assert.Equal(testUser.Lastname, user.Lastname);
//            Assert.Equal(testUser.Username, user.Username);
//            Assert.Equal(testUser.Password, user.Password);
//            Assert.Equal(testUser.Email, user.Email);
//        }
//    }
//}
