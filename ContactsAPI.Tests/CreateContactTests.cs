using ContactsAPI.ApiModels;
using ContactsAPI.Clients;
using ContactsAPI.Controllers;
using ContactsAPI.Repositories;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactsAPI.Tests
{
    public class CreateContactTests: BaseTests
    {
        [Fact]
        public async Task CreateContact()
        {
            // Assert
            var dbName = Guid.NewGuid().ToString();
            var context = CreateContext(dbName);

            var content = Encoding.UTF8.GetBytes("Test image");
            var profileImage = new FormFile(new MemoryStream(content), 0, content.Length, "Data", "image.jpg");
            profileImage.Headers = new HeaderDictionary();
            profileImage.ContentType = "image/jpg";

            var contactDTO = new CreateContactDTO() { Name = "Test", Email = "test@email.com", BirthDate = DateTime.Now, Company = "Test", PersonalPhoneNumber = "11-1111-1112", WorkPhoneNumber = "11-1111-1113", ProfilaImage = profileImage,
                Address = new AddressDTO() 
                { 
                    Street = "Test Street",
                    City = "Test City",
                    State = "Test State",
                    Country = "Test Country"
                } 
            };

            var mockFileClient = new Mock<IFileStorage>();
            mockFileClient.Setup(x => x.SaveFile(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("url");

            var controller = new ContactsController(new ContactsService(new ContactsRepository(context, mockFileClient.Object)));

            var context2 = CreateContext(dbName);

            // Act
            var response = await controller.CreateContact(contactDTO);
            var contacts = await context2.Contacts.ToListAsync();

            // Assert
            Assert.Equal(1, contacts.Count);
            Assert.Equal("url", contacts[0].ProfilaImage);
        }
    }
}
