using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.DTO.ImageDTOs;
using System.Net.Http;

namespace UnitTests.IntegrationTests
{
    public class ImageControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        //private CryptoHubDBContext _context;


        public ImageControllerTests()
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
        public async Task GetById_NoImage()
        {
            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Image/GetById/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);
        }

        [Fact]
        public async Task AddImage()
        {
            //Arrange
            var testImage = new CreateImageDTO()
            {
                Name = "sample",
                Blob = "100110 111010 001011 101001"
            };

            var arrange = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Image/AddImage", testImage);

            Assert.NotNull(arrange);
        }

        [Fact]
        public async Task GetById_Image()
        {
            //Arrange
            var testImage = new CreateImageDTO()
            {
                Name = "sample",
                Blob = "100110 111010 001011 101001"
            };

            await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Image/AddImage", testImage);

            //Act
            var response = await _httpClient.GetAsync("http://localhost:7215/api/Image/GetById/1");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(404, (double)response.StatusCode);

            var image = await response.Content.ReadAsStreamAsync();
            Assert.NotNull(image);
        }

        [Fact]
        public async Task DeleteImage()
        {
            //Arrange
            var testImage = new CreateImageDTO()
            {
                Name = "sample",
                Blob = "100110 111010 001011 101001"
            };

            var arrange = await _httpClient.PostAsJsonAsync("http://localhost:7215/api/Image/AddImage", testImage);

            Assert.NotNull(arrange);

            //Act
            var responseDelete = await _httpClient.DeleteAsync("http://localhost:7215/api/Image/Delete?id=1");

            //Assert
            Assert.NotNull(responseDelete);
            //Assert.Equal(200, (double)responseDelete.StatusCode);

            //Act
            var responseGet = await _httpClient.GetAsync("http://localhost:7215/api/Image/GetById/1");

            //Assert
            Assert.NotNull(responseGet);
            Assert.Equal(404, (double)responseGet.StatusCode);

            var commentsGet = await responseGet.Content.ReadAsStreamAsync();
            Assert.NotNull(commentsGet);
        }
    }
}
