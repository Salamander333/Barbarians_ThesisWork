using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using System.Threading.Tasks;

namespace BarbariansTests.Common.Seeders
{
    public class CraftableWeaponsSeder
    {
        public async Task SeedCraftableWeapons(ApplicationDbContext context)
        {
            var weapon1 = new CraftableWeapon()
            {
                Id = "Id1",
                Name = "Test weapon 1",
                Type = WeaponTypes.Axe,
                MaterialRequired = Materials.Copper,
                MaterialCount = 5,
                Damage = 10,
                CraftCost = 50,
            };

            var weapon2 = new CraftableWeapon()
            {
                Id = "Id2",
                Name = "Test weapon 2",
                Type = WeaponTypes.Staff,
                MaterialRequired = Materials.Mithril,
                MaterialCount = 10,
                Damage = 20,
                CraftCost = 100,
            };

            await context.CraftableWeapons.AddAsync(weapon1);
            await context.CraftableWeapons.AddAsync(weapon2);

            await context.SaveChangesAsync();
        }
    }
}
