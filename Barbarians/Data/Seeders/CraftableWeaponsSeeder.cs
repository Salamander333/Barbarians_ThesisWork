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
                ("Spruce Bow", WeaponTypes.Bow, Materials.Spruce, 10, 50, 50),
                ("Spruce Staff", WeaponTypes.Staff, Materials.Spruce, 10, 55, 60),
                ("Spruce Axe", WeaponTypes.Axe, Materials.Spruce, 12, 70, 80),

                //------------------Tier 2------------------------------------------
                ("Oak Bow", WeaponTypes.Bow, Materials.Oak, 20, 65, 100),
                ("Oak Staff", WeaponTypes.Staff, Materials.Oak, 22, 70, 120),
                ("Copper Sword", WeaponTypes.Sword, Materials.Copper, 20, 110, 250),
                ("Iron Axe", WeaponTypes.Axe, Materials.Iron, 22, 120, 250),

                //------------------Tier 3------------------------------------------
                ("Ash Bow", WeaponTypes.Bow, Materials.Ash, 30, 90, 150),
                ("Ash Staff", WeaponTypes.Staff, Materials.Ash, 30, 85, 140),
                ("Mithril Sword", WeaponTypes.Sword, Materials.Mithril, 35, 160, 350),
                ("Mithril Axe", WeaponTypes.Axe, Materials.Mithril, 35, 180, 400),

                //------------------Балтията------------------------------------------
                ("Балтията на бат Жоро", WeaponTypes.Axe, Materials.Mithril, 50, 250, 1000),
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
