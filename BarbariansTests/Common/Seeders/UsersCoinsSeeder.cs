using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BarbariansTests.Common.Seeders
{
    public class UsersCoinsSeeder
    {
        public async Task SeedUsersWithCoins(ApplicationDbContext context)
        {
            var user1Coins = new Material
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Coins",
                Type = MaterialType.Currency,
                Count = 500,
                Tier = 1,
                UserId = context.Users.Where(x => x.UserName == "User1").SingleOrDefault().Id
            };

            var user2Coins = new Material
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Coins",
                Type = MaterialType.Currency,
                Count = 500,
                Tier = 1,
                UserId = context.Users.Where(x => x.UserName == "User2").SingleOrDefault().Id
            };

            await context.Materials.AddAsync(user1Coins);
            await context.Materials.AddAsync(user2Coins);

            await context.SaveChangesAsync();
        }
    }
}
