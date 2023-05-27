using ContactsAPI.ApiModels;
using ContactsAPI.Models;
using ContactsAPI.Repositories;
using ContactsAPI.Utils;

namespace ContactsAPI.Services
{
    public class ContactsService: IContactsService
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public async Task<GetContactDTO> GetContactById(int id)
        {
            var contact = await _contactsRepository.GetContactById(id);
            if (contact == null)
            {
                return null;
            }
            var contactDTO = contact.MapFromContactToGetContactDTO();
            return contactDTO;
        }

        public async Task<GetContactDTO> GetContactByEmail(string email)
        {
            var contact = await _contactsRepository.GetContactByEmail(email);
            if (contact == null)
            {
                return null;
            }
            var contactDTO = contact.MapFromContactToGetContactDTO();
            return contactDTO;
        }

        public async Task<List<GetContactDTO>> GetContactsByCity(string city)
        {
            var contacts = await _contactsRepository.GetContactsByCity(city);
            var contactsDTO = new List<GetContactDTO>();
            contactsDTO.AddRange(contacts.Select(x => x.MapFromContactToGetContactDTO() ));
            return contactsDTO;
        }

        public async Task<List<GetContactDTO>> GetContactsByState(string state)
        {
            var contacts = await _contactsRepository.GetContactsByState(state);
            var contactsDTO = new List<GetContactDTO>();
            contactsDTO.AddRange(contacts.Select(x => x.MapFromContactToGetContactDTO() ));
            return contactsDTO;
        }

        public async Task<GetContactDTO> GetContactByPhoneNumber(string phoneNumber)
        {
            var contact = await _contactsRepository.GetContactByPhoneNumber(phoneNumber);
            if (contact == null)
            {
                return null;
            }
            var contactDTO = contact.MapFromContactToGetContactDTO();
            return contactDTO;
        }

        public async Task<bool> CreateContact(CreateContactDTO contactDTO)
        {
            if (contactDTO == null)
            {
                return false;
            }

            await _contactsRepository.CreateContact(contactDTO);
            return true;
        }
        

        public async Task<bool> UpdateContact(int id, CreateContactDTO contactDTO)
        {
            var exists = await _contactsRepository.CheckIfContactExistsById(id);
            if (!exists)
            {
                return false;
            }

            await _contactsRepository.UpdateContact(contactDTO, id);
            return true;
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contactDB = await _contactsRepository.GetContactById(id);
            if (contactDB == null)
            {
                return false;
            }
            await _contactsRepository.DeleteContact(contactDB);
            return true;
        }
    }
}
