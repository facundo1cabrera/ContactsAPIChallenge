using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.ApiModels
{
    public class AddressDTO
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
