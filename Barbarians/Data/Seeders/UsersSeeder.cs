using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Barbarians.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbarians.Data.Seeders
{
    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userService = new UsersService(dbContext);

            if (dbContext.Users.Count() >= 3)
            {
                return;
            }

            var users = new List<(string Username, string Email, string Password, string Role, int Health)>
            {
                ("Salamander", "asd@abv.bg", "Asd123", "User", 100),
                ("SalamanderAdmin", "asd@abv.bg", "Asd123", "Admin", 100),
            };

            foreach (var user in users)
            {
                var foo = new ApplicationUser
                {
                    UserName = user.Username,
                    Email = user.Email,
                    Health = user.Health,
                };

                var result = await userManager.CreateAsync(foo, user.Password);

                if (result.Succeeded)
                {
                    string role = IdentityRoles.UserRoleName;
                    if (foo.UserName == "SalamanderAdmin")
                    {
                        role = IdentityRoles.AdministratorRoleName;
                    }
                    else if (foo.UserName == "SalamanderOwner")
                    {
                        role = IdentityRoles.OwnerRoleName;
                    }

                    await userManager.AddToRoleAsync(foo, role);
                    await userService.SeedDatabaseOnSuccessfulRegister(foo.Id);
                }
            }

            //----------Seeding bots---------------
            for (int i = 0; i < 10; i++)
            {
                var foo = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = $"Bot_{i + 1}",
                    Email = $"BotEmail{i + 1}@abv.bg",
                    Health = 100,
                };

                var result = await userManager.CreateAsync(foo, "botPassword1");
                if (result.Succeeded)
                {
                    await userService.SeedDatabaseOnSuccessfulRegister(foo.Id);
                    var coins = dbContext.Materials.Where(x => x.Name == "Coins" && x.UserId == foo.Id).FirstOrDefault();
                    coins.Count = new Random().Next(200, 750);

                    for (int j = 0; j < 3; j++)
                    {
                        var randomArmor = new Random().Next(1, 18);
                        var armor = (CraftableArmor)dbContext.CraftableArmors.Skip(randomArmor).Take(1).FirstOrDefault();
                        var armorToAdd = new Armor
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = armor.Name,
                            Type = armor.Type,
                            Defence = armor.Defence,
                            IsBroken = false,
                            UserId = foo.Id
                        };

                        await dbContext.Armors.AddAsync(armorToAdd);

                        var randomWeapon = new Random().Next(1, 12);
                        var weapon = (CraftableWeapon)dbContext.CraftableWeapons.Skip(randomWeapon).Take(1).FirstOrDefault();
                        var weaponToAdd = new Weapon
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = weapon.Name,
                            Type = weapon.Type,
                            Damage = weapon.Damage,
                            IsBroken = false,
                            UserId = foo.Id
                        };

                        await dbContext.Weapons.AddAsync(weaponToAdd);
                    }
                }
            }
        }
    }
}
