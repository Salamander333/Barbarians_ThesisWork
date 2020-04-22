using System;
using Barbarians.Data;
using Microsoft.EntityFrameworkCore;

namespace BarbariansTests.Common
{
    public class ApplicationDbContextInMemoryFactory
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new ApplicationDbContext(options);
        }
    }
}
