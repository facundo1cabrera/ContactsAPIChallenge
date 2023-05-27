using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Clients
{
    public class AppDbContext: DbContext 
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
