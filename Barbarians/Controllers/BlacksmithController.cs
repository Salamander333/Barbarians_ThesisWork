using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Barbarians.ViewModels.Craftables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Barbarians.Controllers
{
    public class BlacksmithController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _user;

        public BlacksmithController(ApplicationDbContext db, UserManager<ApplicationUser> user)
        {
            this._db = db;
            this._user = user;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index(string craft)
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
                        Name = x.Name,
                        Type = x.Type,
                        MaterialRequired = x.MaterialRequired,
                        MaterialCount = x.MaterialCount,
                        Damage = x.Damage,
                        CraftCost = x.CraftCost
                    })
                    .ToList(),
                    UserMaterials = this._db.Materials.Where(x => x.UserId == _user.GetUserId(this.User)).ToList(),
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
                model.Title = "Leggings";
                model.PartialView.CraftableArmors = model.PartialView.CraftableArmors.Where(x => x.Type == ArmorTypes.Boots).ToList();
                model.PartialName = "_BlacksmithArmors";
            }
            else if (craft == "Weapons")
            {
                model.Title = "Weapons";
                model.PartialName = "_BlacksmithWeapons";
            }
            else
            {
                return this.BadRequest("Not found");
            }


            return this.View(model);
        }
    }
}