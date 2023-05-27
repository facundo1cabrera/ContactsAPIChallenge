using ContactsAPI.Clients;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAPI.Tests
{
    public class BaseTests
    {
        protected AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var dbContext = new AppDbContext(options);
            return dbContext;
        }
    }
}
