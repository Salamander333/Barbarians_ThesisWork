using Barbarians.Data;
using Barbarians.Models;
using System.Threading.Tasks;

namespace BarbariansTests.Common.Seeders
{
    public class UsersSeeder
    {
        public async Task SeedUsers(ApplicationDbContext context)
        {
            var user1 = new ApplicationUser()
            {
                Id = "Id1",
                UserName = "User1",
                Health = 100
            };

            var user2 = new ApplicationUser()
            {
                Id = "Id2",
                UserName = "User2",
                Health = 100
            };

            await context.Users.AddAsync(user1);
            await context.Users.AddAsync(user2);

            await context.SaveChangesAsync();
        }
    }
}
