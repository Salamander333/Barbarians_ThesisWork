using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BarbariansTests.Common.Seeders
{
    public class ArenaSeeder
    {
        public async Task SeedAttackerWithKillingWeapon(ApplicationDbContext context)
        {
            var weapon = new Weapon()
            {
                Id = "Id1",
                Name = "Test weapon 1",
                Type = WeaponTypes.Axe,
                Damage = 1000,
                IsBroken = false,
                UserId = context.Users.Where(x => x.UserName == "User1").SingleOrDefault().Id
            };

            await context.Weapons.AddAsync(weapon);
            await context.SaveChangesAsync();
        }

        public async Task SeedDefendantWithKillingWeapon(ApplicationDbContext context)
        {
            var weapon = new Weapon()
            {
                Id = "Id1",
                Name = "Test weapon 1",
                Type = WeaponTypes.Axe,
                Damage = 1000,
                IsBroken = false,
                UserId = context.Users.Where(x => x.UserName == "User2").SingleOrDefault().Id
            };

            await context.Weapons.AddAsync(weapon);
            await context.SaveChangesAsync();
        }
    }
}
