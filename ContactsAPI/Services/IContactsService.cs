using ContactsAPI.ApiModels;

namespace ContactsAPI.Services
{
    public interface IContactsService
    {
        Task<GetContactDTO> GetContactById(int id);

        Task<GetContactDTO> GetContactByEmail(string email);

        Task<List<GetContactDTO>> GetContactsByCity(string city);

        Task<List<GetContactDTO>> GetContactsByState(string state);

        Task<GetContactDTO> GetContactByPhoneNumber(string phoneNumber);

        Task<bool> CreateContact(CreateContactDTO contactDTO);

        Task<bool> UpdateContact(int id, CreateContactDTO contactDTO);

        Task<bool> DeleteContact(int id);
    }
}
