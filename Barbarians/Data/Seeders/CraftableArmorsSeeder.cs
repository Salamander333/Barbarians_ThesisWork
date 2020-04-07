using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbarians.Data.Seeders
{
    public class CraftableArmorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (dbContext.CraftableArmors.Count() > 1)
            {
                return;
            }

            var armors = new List<(string Name, ArmorTypes Type, Materials MaterialRequired, int MaterialCount, int Defence, int Cost)>
            {
                //------------------Cloth Armor-------------------------------------
                //------------------Tier 1------------------------------------------
                ("Silk Robe", ArmorTypes.Chest, Materials.Silk, 3, 3, 25),
                ("Silk Leggings", ArmorTypes.Leggings, Materials.Silk, 2, 2, 15),
                ("Silk Boots", ArmorTypes.Boots, Materials.Silk, 1, 1, 10),

                //------------------Tier 2------------------------------------------
                ("Linen Robe", ArmorTypes.Chest, Materials.Linen, 5, 4, 35),
                ("Linen Leggings", ArmorTypes.Leggings, Materials.Linen, 3, 3, 25),
                ("LenLinenin Boots", ArmorTypes.Boots, Materials.Linen, 2, 1, 15),

                //------------------Tier 3------------------------------------------
                ("Jute Robe", ArmorTypes.Chest, Materials.Jute, 6, 5, 45),
                ("Jute Leggings", ArmorTypes.Leggings, Materials.Jute, 5, 4, 30),
                ("Jute Boots", ArmorTypes.Boots, Materials.Jute, 2, 3, 20),

                //------------------Metal Armor-------------------------------------
                //------------------Tier 1------------------------------------------
                ("Copper Robe", ArmorTypes.Chest, Materials.Copper, 5, 8, 70),
                ("Copper Leggings", ArmorTypes.Leggings, Materials.Copper, 3, 6, 50),
                ("Copper Boots", ArmorTypes.Boots, Materials.Copper, 2, 2, 30),

                //------------------Tier 2------------------------------------------
                ("Iron Robe", ArmorTypes.Chest, Materials.Iron, 6, 12, 95),
                ("Iron Leggings", ArmorTypes.Leggings, Materials.Iron, 5, 10, 65),
                ("Iron Boots", ArmorTypes.Boots, Materials.Iron, 2, 8, 45),

                //------------------Tier 3------------------------------------------
                ("Mithril Robe", ArmorTypes.Chest, Materials.Mithril, 3, 15, 150),
                ("Mithril Leggings", ArmorTypes.Leggings, Materials.Mithril, 2, 12, 75),
                ("Mithril Boots", ArmorTypes.Boots, Materials.Mithril, 1, 10, 45),
            };

            foreach (var armor in armors)
            {
                await dbContext.CraftableArmors.AddAsync(new CraftableArmor 
                { 
                    Id = Guid.NewGuid().ToString(),
                    Name = armor.Name,
                    Type = armor.Type,
                    MaterialRequired = armor.MaterialRequired,
                    MaterialCount = armor.MaterialCount,
                    Defence = armor.Defence,
                    CraftCost = armor.Cost
                });
            }
        }
    }
}
