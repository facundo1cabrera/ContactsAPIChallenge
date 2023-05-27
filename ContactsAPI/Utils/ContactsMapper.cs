using ContactsAPI.ApiModels;
using ContactsAPI.Models;

namespace ContactsAPI.Utils
{
    public static class ContactsMapper
    {
        public static GetContactDTO MapFromContactToGetContactDTO(this Contact contact)
        {
            var getContactDTO = new GetContactDTO()
            {
                Id = contact.Id,
                BirthDate = contact.BirthDate,
                Company = contact.Company,
                Email = contact.Email,
                Name = contact.Name,
                PersonalPhoneNumber = contact.PersonalPhoneNumber,
                WorkPhoneNumber = contact.WorkPhoneNumber,
                ProfilaImageUrl = contact.ProfilaImage,
                Adress = new AddressDTO()
                {
                    Street = contact.Adress.Street,
                    City = contact.Adress.City,
                    State = contact.Adress.State,
                    Country = contact.Adress.Country
                }
            };
            return getContactDTO;
        }

        public static Contact MapFromCreateContactDTOToContact(this CreateContactDTO contactDTO)
        {
            var contact = new Contact()
            {
                Adress = new Address()
                {
                    Street = contactDTO.Address.Street,
                    City = contactDTO.Address.City,
                    State = contactDTO.Address.State,
                    Country = contactDTO.Address.Country
                },
                BirthDate = contactDTO.BirthDate,
                Email = contactDTO.Email,
                Company = contactDTO.Company,
                Name = contactDTO.Name,
                PersonalPhoneNumber = contactDTO.PersonalPhoneNumber,
                WorkPhoneNumber = contactDTO.WorkPhoneNumber
            };

            return contact;
        }
    }
}
