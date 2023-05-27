using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.ApiModels
{
    public class CreateContactDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public IFormFile ProfilaImage { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [Phone]
        public string PersonalPhoneNumber { get; set; }
        [Required]
        [Phone]
        public string WorkPhoneNumber { get; set; }
        [Required]
        public AddressDTO Address { get; set; }
    }
}
