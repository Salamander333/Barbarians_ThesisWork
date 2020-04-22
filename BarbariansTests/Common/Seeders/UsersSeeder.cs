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
                UserName = "User1",
            };

            var user2 = new ApplicationUser()
            {
                UserName = "User2",
            };

            await context.Users.AddAsync(user1);
            await context.Users.AddAsync(user2);

            await context.SaveChangesAsync();
        }
    }
}
