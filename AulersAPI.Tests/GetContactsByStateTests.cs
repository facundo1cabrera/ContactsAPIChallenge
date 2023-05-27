using ContactsAPI.ApiModels;
using ContactsAPI.Controllers;
using ContactsAPI.Models;
using ContactsAPI.Repositories;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactsAPI.Tests
{
    public class GetContactsByStateTests
    {
        [Fact]
        public async void GetContactsByState_should_return_contacts_from_repository()
        {
            // Arrange
            var mockRepo = new Mock<IContactsRepository>();
            mockRepo.Setup(x => x.GetContactsByState(It.IsAny<string>()))
                .ReturnsAsync(new List<Contact> {
                    new Contact()
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
                    },
                    new Contact()
                    {
                        Id = 2,
                        Email = "test2@mail.com",
                        BirthDate = DateTime.Parse("2023-05-27T20:01:08.071Z"),
                        Company = "test",
                        PersonalPhoneNumber = "11 1111 1113",
                        WorkPhoneNumber = "11 1111 1114",
                        Name = "Test name",
                        ProfilaImage = "Test image"
                        ,
                        Address = new Address() { Id = 1, Street = "Test Street", City = "Test City", State = "Test State", Country = "Test Country" }
                    }
                });

            var controller = new ContactsController(new ContactsService(mockRepo.Object));

            // Act
            var response = await controller.GetContactsByState("1111111111") as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<GetContactDTO>>(response.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async void GetContactsByState_should_return_an_empty_array_when_repo_does_not_have_contacts()
        {
            // Arrange
            var mockRepo = new Mock<IContactsRepository>();
            mockRepo.Setup(x => x.GetContactsByState(It.IsAny<string>()))
                .ReturnsAsync(new List<Contact> { });

            var controller = new ContactsController(new ContactsService(mockRepo.Object));

            // Act
            var response = await controller.GetContactsByState("1111111111") as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<GetContactDTO>>(response.Value);
            Assert.Equal(0, items.Count);
        }
    }
}
