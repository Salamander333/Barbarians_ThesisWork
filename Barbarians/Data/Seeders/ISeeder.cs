using System;
using System.Threading.Tasks;

namespace Barbarians.Data.Seeders
{
    interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
