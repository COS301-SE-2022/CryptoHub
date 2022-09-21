using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
//using Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.UserDTOs;


namespace UnitTests.IntegrationTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        //private CryptoHubDBContext _context;


        public UserControllerTests()
        {
            var dbname = Guid.NewGuid().ToString();
            var appFactory = new WebApplicationFactory<Program>()
                             .WithWebHostBuilder(builder =>
                             {
                                 builder.ConfigureServices(
                                     services =>
                                     {
                                         var descriptor = services.SingleOrDefault(
                                             d => d.ServiceType == typeof(DbContextOptions<CryptoHubDBContext>));

                                         if (descriptor != null)
                                         {
                                             services.Remove(descriptor);
                                         }
                                         services.AddDbContext<CryptoHubDBContext>(
                                             options =>
                                             {
                                                 options.UseInMemoryDatabase(dbname);
                                             });
                                     });
                             });

            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllUsers_NoUsers()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var users = await response.Content.ReadAsAsync<List<User>>();

            Assert.Equal(0, users.Count());
        }

        [Fact]
        public async Task GetAllUsers_Users()
        {
            //Arrange
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };
            var testUser2 = new RegisterDTO()
            {

                Firstname = "test2",
                Lastname = "user2",
                Username = "user2",
                Email = "test2@gmail.com",
                Password = "1234"

            };
            var testUser3 = new RegisterDTO()
            {

                Firstname = "test3",
                Lastname = "user3",
                Username = "user3",
                Email = "test3@gmail.com",
                Password = "1234"

            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var users = await response.Content.ReadAsAsync<List<User>>();

            Assert.Equal(3, users.Count);
        }

        [Fact]
        public async Task GetUserById_NoUsers()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserById/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);
        }

        [Fact]
        public async Task GetUserById_UserFound()
        {
            //Arrange
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "user1",
                Email = "test@gmail.com",
                Password = "1234",
            };
            var testUser2 = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "user2",
                Email = "test2@gmail.com",
                Password = "1234",
            };
            var testUser3 = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "user3",
                Email = "test3@gmail.com",
                Password = "1234",
            };

            var x = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var y = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
            var z = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserById/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var user = await response.Content.ReadAsAsync<User>();

            Assert.NotNull(user);
            Assert.Equal(testUser.Firstname, user.Firstname);
            Assert.Equal(testUser.Lastname, user.Lastname);
            Assert.Equal(testUser.Username, user.Username);
        }

        [Fact]
        public async Task GetUserById_UserNotFound()
        {
            //Arrange
            var testUser = new User()
            {
                UserId = 1,
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };
            var testUser2 = new User()
            {
                UserId = 2,
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test2@gmail.com",
                Password = "1234"
            };
            var testUser3 = new User()
            {
                UserId = 3,
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test3@gmail.com",
                Password = "1234"
            };

            var x = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var y = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
            var z = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserById/4");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);
        }

        [Fact]
        public async Task AddUser()
        {
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };

            //Act
            var add = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert
            Assert.NotNull(add);

            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var user = await response.Content.ReadAsAsync<List<User>>();

            Assert.NotNull(user);
            Assert.Equal(testUser.Firstname, user.First().Firstname);
            Assert.Equal(testUser.Lastname, user.First().Lastname);
            Assert.Equal(testUser.Username, user.First().Username);
        }

        [Fact]
        public async Task GetUserByEmail_UserNotFound()
        {
            //Arrange
            var testUser = new User()
            {
                UserId = 1,
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };
            var testUser2 = new User()
            {
                UserId = 2,
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test2@gmail.com",
                Password = "1234"
            };
            var testUser3 = new User()
            {
                UserId = 3,
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test3@gmail.com",
                Password = "1234"
            };

            var x = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var y = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
            var z = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserByEmail/test4@gmail.com");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);
        }

        [Fact]
        public async Task GetUserByEmail_UserFound()
        {
            //Arrange
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "user1",
                Email = "test@gmail.com",
                Password = "1234",
            };
            var testUser2 = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "user2",
                Email = "test2@gmail.com",
                Password = "1234",
            };
            var testUser3 = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "user3",
                Email = "test3@gmail.com",
                Password = "1234",
            };

            var x = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var y = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser2);
            var z = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser3);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetUserByEmail/test@gmail.com");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var user = await response.Content.ReadAsAsync<User>();

            Assert.NotNull(user);
            Assert.Equal(testUser.Firstname, user.Firstname);
            Assert.Equal(testUser.Lastname, user.Lastname);
            Assert.Equal(testUser.Username, user.Username);
        }

        [Fact]
        public async Task UpdateUser_UserDoesNotExist()
        {
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };
            var testUserUpdate = new User()
            {
                UserId = 999,
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "update@gmail.com",
                Password = "1234"
            };

            //Act
            var add = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert
            Assert.NotNull(add);

            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var user = await response.Content.ReadAsAsync<List<User>>();

            Assert.NotNull(user);
            Assert.Equal(testUser.Firstname, user.First().Firstname);
            Assert.Equal(testUser.Lastname, user.First().Lastname);
            Assert.Equal(testUser.Username, user.First().Username);

            //Act 2
            var update = await _httpClient.PutAsJsonAsync("http://localhost:7215/api/User/UpdateUser", testUserUpdate);

            //Assert 2
            Assert.NotNull(update);

            Assert.NotNull(update);
            Assert.Equal(400, (double)update.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_UserExists()
        {
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };
            var testUserUpdate = new User()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "update@gmail.com",
                Password = "1234"
            };

            //Act
            var add = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert
            Assert.NotNull(add);

            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var user = await response.Content.ReadAsAsync<List<User>>();

            Assert.NotNull(user);
            Assert.Equal(testUser.Firstname, user.First().Firstname);
            Assert.Equal(testUser.Lastname, user.First().Lastname);
            Assert.Equal(testUser.Username, user.First().Username);

            //Act 2
            var update = await _httpClient.PutAsJsonAsync("http://localhost:7215/api/User/UpdateUser", testUserUpdate);
            var response2 = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert 2
            Assert.NotNull(update);

            Assert.NotNull(response2);
            Assert.Equal(200, (double)response2.StatusCode);

            var user2 = await response2.Content.ReadAsAsync<List<User>>();

            Assert.NotNull(user);
            Assert.Equal(testUserUpdate.Firstname, user2.First().Firstname);
            Assert.Equal(testUserUpdate.Lastname, user2.First().Lastname);
            Assert.Equal(testUserUpdate.Username, user2.First().Username);
        }

        [Fact]
        public async Task Delete()
        {
            var testUser = new RegisterDTO()
            {
                Firstname = "test",
                Lastname = "user",
                Username = "test user",
                Email = "test@gmail.com",
                Password = "1234"
            };

            //Act
            var add = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/User/AddUser", testUser);
            var response = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert
            Assert.NotNull(add);

            Assert.NotNull(response);
            Assert.Equal(200, (double)response.StatusCode);

            var user = await response.Content.ReadAsAsync<List<User>>();

            Assert.NotNull(user);
            Assert.Equal(testUser.Firstname, user.First().Firstname);
            Assert.Equal(testUser.Lastname, user.First().Lastname);
            Assert.Equal(testUser.Username, user.First().Username);

            //Act 2
            var responseDelete = await _httpClient.DeleteAsync("http://localhost:7215/api/User/Delete?id=1");

            //Assert 2
            Assert.NotNull(responseDelete);
            Assert.Equal(200, (double)responseDelete.StatusCode);

            //Act 3
            var responseGet = await _httpClient.GetAsync("http://localhost:7215/api/User/GetAllUsers");

            //Assert 3
            Assert.NotNull(responseGet);
            Assert.Equal(200, (double)responseGet.StatusCode);

            var commentsGet = await responseGet.Content.ReadAsAsync<List<User>>();

            Assert.Empty(commentsGet);

        }

        //[Fact]
        //public async Task GetAllUsersFollowingCoin_NoUsers()
        //{

        //}

    }
}
