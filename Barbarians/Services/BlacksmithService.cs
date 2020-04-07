using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Barbarians.ViewModels.Craftables;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Barbarians.Services
{
    public class BlacksmithService : IBlacksmithService
    {
        private readonly ApplicationDbContext _db;

        public BlacksmithService(ApplicationDbContext db, UserManager<ApplicationUser> user)
        {
            this._db = db;
        }

        public async Task AddArmorItemToUser(string id, string userId)
        {
            var itemFromDb = _db.CraftableArmors.SingleOrDefault(x => x.Id == id);
            var itemToAdd = new Armor
            {
                Id = Guid.NewGuid().ToString(),
                Name = itemFromDb.Name,
                Defence = itemFromDb.Defence,
                IsBroken = false,
                UserId = userId
            };

            var userMaterial = _db.Materials.Where(x => x.UserId == userId).FirstOrDefault(x => x.Name == itemFromDb.MaterialRequired.ToString());
            userMaterial.Count -= itemFromDb.MaterialCount;

            var userCoins = _db.Materials.Where(x => x.UserId == userId).FirstOrDefault(x => x.Name == "Coins");
            userCoins.Count -= itemFromDb.CraftCost;

            await _db.Armors.AddAsync(itemToAdd);
            await _db.SaveChangesAsync();
        }

        public Task AddWeaponItemToUser(string id, string userId)
        {
            throw new System.NotImplementedException();
        }

        public CraftableModel CreateCraftableModel(string craft, string userId)
        {
            var model = new CraftableModel
            {
                Title = "",
                PartialName = "",
                PartialView = new CraftableModelForPartial
                {
                    CraftableArmors = _db.CraftableArmors
                     .Select(x => new CraftableArmorViewModel
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Type = x.Type,
                         MaterialRequired = x.MaterialRequired,
                         MaterialCount = x.MaterialCount,
                         Defence = x.Defence,
                         CraftCost = x.CraftCost,
                     })
                     .ToList(),
                    CraftableWeapons = _db.CraftableWeapons
                     .Select(x => new CraftableWeaponViewModel
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Type = x.Type,
                         MaterialRequired = x.MaterialRequired,
                         MaterialCount = x.MaterialCount,
                         Damage = x.Damage,
                         CraftCost = x.CraftCost
                     })
                     .ToList(),
                    UserMaterials = this._db.Materials.Where(x => x.UserId == userId).ToList(),
                },
            };

            if (craft == "Chest")
            {
                model.Title = "Chest";
                model.PartialView.CraftableArmors = model.PartialView.CraftableArmors.Where(x => x.Type == ArmorTypes.Chest).ToList();
                model.PartialName = "_BlacksmithArmors";
            }
            else if (craft == "Leggings")
            {
                model.Title = "Leggings";
                model.PartialView.CraftableArmors = model.PartialView.CraftableArmors.Where(x => x.Type == ArmorTypes.Leggings).ToList();
                model.PartialName = "_BlacksmithArmors";
            }
            else if (craft == "Boots")
            {
                model.Title = "Boots";
                model.PartialView.CraftableArmors = model.PartialView.CraftableArmors.Where(x => x.Type == ArmorTypes.Boots).ToList();
                model.PartialName = "_BlacksmithArmors";
            }
            else if (craft == "Weapons")
            {
                model.Title = "Weapons";
                model.PartialName = "_BlacksmithWeapons";
            }
            

            return model;
        }
    }
}
