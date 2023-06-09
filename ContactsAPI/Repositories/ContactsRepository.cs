﻿using ContactsAPI.ApiModels;
using ContactsAPI.Clients;
using ContactsAPI.Models;
using ContactsAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Repositories
{
    public class ContactsRepository: IContactsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileStorage _fileStorage;

        public ContactsRepository(AppDbContext dbContext, IFileStorage fileStorage)
        {
            _dbContext = dbContext;
            _fileStorage = fileStorage;
        }

        public async Task<Contact> GetContactById(int id)
        {
            var contact = await _dbContext.Contacts.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
            return contact;
        }

        public async Task<Contact> GetContactByEmail(string email)
        {
            var contact = await _dbContext.Contacts.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
            return contact;
        }

        public async Task<List<Contact>> GetContactsByCity(string city)
        {
            var contacts = await _dbContext.Contacts.Include(x => x.Address).Where(x => x.Address.City == city).ToListAsync();
            return contacts;
        }

        public async Task<List<Contact>> GetContactsByState(string state)
        {
            var contacts = await _dbContext.Contacts.Include(x => x.Address).Where(x => x.Address.State == state).ToListAsync();
            return contacts;
        }

        public async Task<Contact> GetContactByPhoneNumber(string phoneNumber)
        {
            var contact = await _dbContext.Contacts.Include(x => x.Address).FirstOrDefaultAsync(x => x.PersonalPhoneNumber == phoneNumber || x.WorkPhoneNumber == phoneNumber);
            return contact;
        }

        public async Task CreateContact(CreateContactDTO contactDTO)
        {
            var contact = contactDTO.MapFromCreateContactDTOToContact();

            using (var memoryStream = new MemoryStream())
            {
                await contactDTO.ProfilaImage.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(contactDTO.ProfilaImage.FileName);
                contact.ProfilaImage = await _fileStorage.SaveFile(content, extension, _fileStorage.GetContainerName(),
                    contactDTO.ProfilaImage.ContentType);
            }
            
            _dbContext.Add(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateContact(CreateContactDTO contactDTO, int id)
        {
            var contactDB = await _dbContext.Contacts.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);

            if (contactDB == null)
            {
                return;
            }

            contactDB.Email = contactDTO.Email;
            contactDB.WorkPhoneNumber = contactDTO.WorkPhoneNumber;
            contactDB.PersonalPhoneNumber = contactDTO.PersonalPhoneNumber;
            contactDB.BirthDate = contactDTO.BirthDate;
            contactDB.Company = contactDTO.Company;
            contactDB.Name = contactDTO.Name;
            contactDB.Address.City = contactDTO.Address.City;
            contactDB.Address.State = contactDTO.Address.State;
            contactDB.Address.Street = contactDTO.Address.Street;
            contactDB.Address.Country = contactDTO.Address.Country;

            using (var memoryStream = new MemoryStream())
            {
                await contactDTO.ProfilaImage.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(contactDTO.ProfilaImage.FileName);
                contactDB.ProfilaImage = await _fileStorage.EditFile(content, extension, _fileStorage.GetContainerName(), contactDB.ProfilaImage,
                    contactDTO.ProfilaImage.ContentType);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteContact(Contact contact)
        {
            _dbContext.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfContactExistsById(int id)
        {
            var exists = await _dbContext.Contacts.AnyAsync(x => x.Id == id);
            return exists;
        }
    }
}
