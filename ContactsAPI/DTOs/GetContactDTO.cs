namespace ContactsAPI.ApiModels
{
    public class GetContactDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string ProfilaImageUrl { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public AddressDTO Adress { get; set; }
    }
}
