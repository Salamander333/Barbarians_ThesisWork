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
    public class CraftableWeaponsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (dbContext.CraftableWeapons.Count() > 1)
            {
                return;
            }

            var weapons = new List<(string Name, WeaponTypes Type, Materials MaterialRequired, int MaterialCount, int Damage, int Cost)>
            {
                //------------------Tier 1------------------------------------------
                ("Spruce Bow", WeaponTypes.Bow, Materials.Spruce, 3, 10, 20),
                ("Spruce Staff", WeaponTypes.Staff, Materials.Spruce, 2, 8, 18),
                ("Spruce Axe", WeaponTypes.Axe, Materials.Spruce, 2, 12, 28),

                //------------------Tier 2------------------------------------------
                ("Oak Bow", WeaponTypes.Bow, Materials.Oak, 3, 14, 30),
                ("Oak Staff", WeaponTypes.Staff, Materials.Oak, 2, 12, 28),
                ("Copper Sword", WeaponTypes.Sword, Materials.Copper, 5, 18, 30),
                ("Iron Axe", WeaponTypes.Axe, Materials.Iron, 4, 20, 50),

                //------------------Tier 3------------------------------------------
                ("Ash Bow", WeaponTypes.Bow, Materials.Ash, 5, 18, 60),
                ("Ash Staff", WeaponTypes.Staff, Materials.Ash, 5, 17, 55),
                ("Mithril Sword", WeaponTypes.Sword, Materials.Mithril, 7, 25, 80),
                ("Mithril Axe", WeaponTypes.Axe, Materials.Mithril, 7, 28, 100),
            };

            foreach (var weapon in weapons)
            {
                await dbContext.CraftableWeapons.AddAsync(new CraftableWeapon
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = weapon.Name,
                    Type = weapon.Type,
                    MaterialRequired = weapon.MaterialRequired,
                    MaterialCount = weapon.MaterialCount,
                    Damage = weapon.Damage,
                    CraftCost = weapon.Cost
                });
            }
        }
    }
}
