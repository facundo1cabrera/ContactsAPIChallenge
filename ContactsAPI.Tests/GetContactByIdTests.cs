using ContactsAPI.ApiModels;
using ContactsAPI.Controllers;
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
    public class GetContactByIdTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetContactByIdTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async void GetContactById_should_return_contact_from_repository()
        {
            // Arrange
            var mockRepo = new Mock<IContactsRepository>();
            mockRepo.Setup(x => x.GetContactById(It.IsAny<int>()))
                .ReturnsAsync(new Contact()
                    {
                        Id = 1,
                        Email = "test@mail.com",
                        BirthDate = DateTime.Parse("2023-05-27T20:01:08.071Z"),
                        Company = "test",
                        PersonalPhoneNumber = "11 1111 1111",
                        WorkPhoneNumber = "11 1111 1112",
                        Name = "Test name",
                        ProfilaImage = "Test image"
                        ,
                        Address = new Address() { Id = 1, Street = "Test Street", City = "Test City", State = "Test State", Country = "Test Country" }
                    });

            var controller = new ContactsController(new ContactsService(mockRepo.Object));

            // Act
            var response = await controller.GetContactById(1) as OkObjectResult;

            // Assert
            var item = Assert.IsType<GetContactDTO>(response.Value);
        }

        [Fact]
        public async void GetContactById_should_return_404_when_repo_does_not_have_contacts()
        {
            // Arrange
            var mockRepo = new Mock<IContactsRepository>();
            mockRepo.Setup(x => x.GetContactById(It.IsAny<int>()))
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
            var response = await client.GetAsync("api/contacts/id/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
