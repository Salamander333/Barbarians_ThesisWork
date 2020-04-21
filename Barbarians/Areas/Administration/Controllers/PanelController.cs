using Barbarians.Areas.Administration.ViewModels.Admin;
using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Barbarians.Areas.Administration.Controllers
{
    [Authorize(Roles = IdentityRoles.AdministratorRoleName)]
    [Area("Administration")]
    public class PanelController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PanelController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddArmor()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWeapon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddArmor(AdminAddArmorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var armor = new CraftableArmor
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Type = model.Type,
                    MaterialRequired = model.MaterialRequired,
                    MaterialCount = model.MaterialCount,
                    Defence = model.Defence,
                    CraftCost = model.Cost
                };

                await _db.CraftableArmors.AddAsync(armor);
                await _db.SaveChangesAsync();

                return this.RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid armor entry.");
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddWeapon(AdminAddWeaponViewModel model)
        {
            if (ModelState.IsValid)
            {
                var weapon = new CraftableWeapon
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Type = model.Type,
                    MaterialRequired = model.MaterialRequired,
                    MaterialCount = model.MaterialCount,
                    Damage = model.Damage,
                    CraftCost = model.Cost
                };

                await _db.CraftableWeapons.AddAsync(weapon);
                await _db.SaveChangesAsync();

                return this.RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid armor entry.");
            return this.View(model);
        }
    }
}