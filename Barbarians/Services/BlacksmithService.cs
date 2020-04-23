using AutoMapper;
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
        private readonly IMapper _mapper;

        public BlacksmithService(ApplicationDbContext db, UserManager<ApplicationUser> user, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task AddArmorItemToUser(string id, string userId)
        {
            var itemFromDb = _db.CraftableArmors.SingleOrDefault(x => x.Id == id);
            var itemToAdd = new Armor
            {
                Id = Guid.NewGuid().ToString(),
                Name = itemFromDb.Name,
                Type = itemFromDb.Type,
                Defence = itemFromDb.Defence,
                IsBroken = false,
                UserId = userId
            };

            var userMaterial = _db.Materials.Where(x => x.UserId == userId).FirstOrDefault(x => x.Name == itemFromDb.MaterialRequired.ToString());
            if (userMaterial != null) { userMaterial.Count -= itemFromDb.MaterialCount; }

            var userCoins = _db.Materials.Where(x => x.UserId == userId).FirstOrDefault(x => x.Name == "Coins");
            if (userCoins != null) { userCoins.Count -= itemFromDb.CraftCost; }

            await _db.Armors.AddAsync(itemToAdd);
            await _db.SaveChangesAsync();
        }

        public async Task AddWeaponItemToUser(string id, string userId)
        {
            var itemFromDb = _db.CraftableWeapons.SingleOrDefault(x => x.Id == id);
            var itemToAdd = new Weapon
            {
                Id = Guid.NewGuid().ToString(),
                Name = itemFromDb.Name,
                Type = itemFromDb.Type,
                Damage = itemFromDb.Damage,
                IsBroken = false,
                UserId = userId
            };

            var userMaterial = _db.Materials.Where(x => x.UserId == userId).FirstOrDefault(x => x.Name == itemFromDb.MaterialRequired.ToString());
            if (userMaterial != null) { userMaterial.Count -= itemFromDb.MaterialCount; }

            var userCoins = _db.Materials.Where(x => x.UserId == userId).FirstOrDefault(x => x.Name == "Coins");
            if (userCoins != null) { userCoins.Count -= itemFromDb.CraftCost; }

            await _db.Weapons.AddAsync(itemToAdd);
            await _db.SaveChangesAsync();
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
                     .Select(x =>
                        _mapper.Map<CraftableArmorViewModel>(x)
                     )
                     .ToList(),
                    CraftableWeapons = _db.CraftableWeapons
                     .Select(x =>
                        _mapper.Map<CraftableWeaponViewModel>(x)
                     )
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
