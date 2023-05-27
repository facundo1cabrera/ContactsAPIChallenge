using ContactsAPI.ApiModels;
using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public interface IContactsRepository
    {
        Task<Contact> GetContactById(int id);
        Task<Contact> GetContactByEmail(string email);
        Task<List<Contact>> GetContactsByCity(string city);
        Task<List<Contact>> GetContactsByState(string state);
        Task<Contact> GetContactByPhoneNumber(string phoneNumber);
        Task CreateContact(CreateContactDTO contact);
        Task UpdateContact(CreateContactDTO contact, int id);
        Task DeleteContact(Contact contact);
        Task<bool> CheckIfContactExistsById(int id);

    }
}
