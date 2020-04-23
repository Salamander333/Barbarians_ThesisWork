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
                ("Silk Robe", ArmorTypes.Chest, Materials.Silk, 10, 15, 50),
                ("Silk Leggings", ArmorTypes.Leggings, Materials.Silk, 7, 7, 35),
                ("Silk Boots", ArmorTypes.Boots, Materials.Silk, 4, 5, 20),

                //------------------Tier 2------------------------------------------
                ("Linen Robe", ArmorTypes.Chest, Materials.Linen, 15, 25, 80),
                ("Linen Leggings", ArmorTypes.Leggings, Materials.Linen, 10, 15, 50),
                ("Linen Boots", ArmorTypes.Boots, Materials.Linen, 8, 7, 30),

                //------------------Tier 3------------------------------------------
                ("Jute Robe", ArmorTypes.Chest, Materials.Jute, 20, 35, 100),
                ("Jute Leggings", ArmorTypes.Leggings, Materials.Jute, 15, 25, 80),
                ("Jute Boots", ArmorTypes.Boots, Materials.Jute, 10, 14, 35),

                //------------------Metal Armor-------------------------------------
                //------------------Tier 1------------------------------------------
                ("Copper Robe", ArmorTypes.Chest, Materials.Copper, 10, 30, 150),
                ("Copper Leggings", ArmorTypes.Leggings, Materials.Copper, 7, 15, 80),
                ("Copper Boots", ArmorTypes.Boots, Materials.Copper, 4, 10, 45),

                //------------------Tier 2------------------------------------------
                ("Iron Robe", ArmorTypes.Chest, Materials.Iron, 15, 45, 200),
                ("Iron Leggings", ArmorTypes.Leggings, Materials.Iron, 10, 27, 100),
                ("Iron Boots", ArmorTypes.Boots, Materials.Iron, 8, 15, 60),

                //------------------Tier 3------------------------------------------
                ("Mithril Robe", ArmorTypes.Chest, Materials.Mithril, 20, 60, 260),
                ("Mithril Leggings", ArmorTypes.Leggings, Materials.Mithril, 15, 35, 110),
                ("Mithril Boots", ArmorTypes.Boots, Materials.Mithril, 10, 20, 90),
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
