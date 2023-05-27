using ContactsAPI.ApiModels;
using ContactsAPI.Models;
using ContactsAPI.Repositories;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactsAPI.Tests
{
    public class GetContactByPhoneNumberTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetContactByPhoneNumberTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void GetContactByPhoneNumber_should_return_contacts_from_repository()
        {
            // Arrange
            var mockRepo = new Mock<IContactsRepository>();
            mockRepo.Setup(x => x.GetContactByPhoneNumber(It.IsAny<string>()))
                .ReturnsAsync(new Contact() { Id = 1, Email = "test@mail.com", BirthDate = DateTime.Parse("2023-05-27T20:01:08.071Z"), Company = "test", PersonalPhoneNumber = "11 1111 1111", WorkPhoneNumber = "11 1111 1112", Name = "Test name", ProfilaImage = "Test image"
                    ,Address = new Address() { Id = 1, Street = "Test Street", City = "Test City", State = "Test State", Country = "Test Country" }
                });

            var expectedContent = "{\"id\":1,\"name\":\"Test name\",\"company\":\"test\",\"profilaImageUrl\":\"Test image\",\"email\":\"test@mail.com\",\"birthDate\":\"2023-05-27T17:01:08.071-03:00\",\"personalPhoneNumber\":\"11 1111 1111\",\"workPhoneNumber\":\"11 1111 1112\",\"adress\":{\"street\":\"Test Street\",\"city\":\"Test City\",\"state\":\"Test State\",\"country\":\"Test Country\"}}";

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IContactsService, ContactsService>();
                    services.AddSingleton(mockRepo.Object);
                });

            }).CreateClient(new WebApplicationFactoryClientOptions());

            // Act
            var response = await client.GetAsync("api/contacts/phone/1");
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedContent, responseContent);
        }

        [Fact]
        public async void GetContactByPhoneNumber_should_return_404_when_repository_has_no_contacts()
        {
            // Arrange
            var mockRepo = new Mock<IContactsRepository>();
            mockRepo.Setup(x => x.GetContactByPhoneNumber(It.IsAny<string>()))
                .ReturnsAsync(null as Contact);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IContactsService, ContactsService>();
                    services.AddSingleton(mockRepo.Object);
                });

            }).CreateClient(new WebApplicationFactoryClientOptions());

            // Act
            var response = await client.GetAsync("api/contacts/phone/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
