namespace ContactsAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string ProfilaImage { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public Address Adress { get; set; }
    }
}
