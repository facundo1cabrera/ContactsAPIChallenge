using ContactsAPI.ApiModels;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet("id/{id:int}")]
        public async Task<ActionResult<GetContactDTO>> GetContactById(int id)
        {
            var contact = await _contactsService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<GetContactDTO>> GetContactByEmail(string email)
        {
            var contact = await _contactsService.GetContactByEmail(email);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<List<GetContactDTO>>> GetContactsByCity(string city)
        {
            var contact = await _contactsService.GetContactsByCity(city);
            return Ok(contact);
        }

        [HttpGet("state/{state}")]
        public async Task<ActionResult<List<GetContactDTO>>> GetContactsByState(string state)
        {
            var contact = await _contactsService.GetContactsByState(state);
            return Ok(contact);
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<ActionResult<List<GetContactDTO>>> GetContactsByPhoneNumber(string phoneNumber)
        {
            var contact = await _contactsService.GetContactByPhoneNumber(phoneNumber);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact([FromForm] CreateContactDTO createContactDTO)
        {
            var created = await _contactsService.CreateContact(createContactDTO);
            if (!created)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateContact(int id,[FromForm] CreateContactDTO contactDTO)
        {
            var updated = await _contactsService.UpdateContact(id, contactDTO);
            if (!updated)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var deleted = await _contactsService.DeleteContact(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
