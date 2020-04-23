using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using System.Threading.Tasks;

namespace BarbariansTests.Common.Seeders
{
    public class CraftableArmorsSeeder
    {
        public async Task SeedCraftableArmors(ApplicationDbContext context)
        {
            var armor1 = new CraftableArmor()
            {
                Id = "Id1",
                Name = "Test armor 1",
                Type =ArmorTypes.Chest,
                MaterialRequired = Materials.Copper,
                MaterialCount = 5,
                Defence = 10,
                CraftCost = 50,
            };

            var armor2 = new CraftableArmor()
            {
                Id = "Id2",
                Name = "Test armor 2",
                Type = ArmorTypes.Boots,
                MaterialRequired = Materials.Mithril,
                MaterialCount = 10,
                Defence = 20,
                CraftCost = 100,
            };

            await context.CraftableArmors.AddAsync(armor1);
            await context.CraftableArmors.AddAsync(armor2);

            await context.SaveChangesAsync();
        }
    }
}
