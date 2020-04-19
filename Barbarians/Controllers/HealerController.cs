using Barbarians.Data;
using Barbarians.Models;
using Barbarians.ViewModels.Healer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Barbarians.Controllers
{
    public class HealerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _user;

        public HealerController(ApplicationDbContext db, UserManager<ApplicationUser> user)
        {
            this._db = db;
            this._user = user;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var user = _db.ApplicationUsers.Where(x => x.Id == _user.GetUserId(this.User)).SingleOrDefault();

            var model = new HealerCharacterViewModel
            {
                MissingHP = 100 - user.Health,
            };

            model.IsFullLife = model.MissingHP > 0 ? false : true;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Heal()
        {
            var user = _db.ApplicationUsers.Where(x => x.Id == _user.GetUserId(this.User)).SingleOrDefault();

            var model = new HealerCharacterViewModel
            {
                MissingHP = 100 - user.Health,
            };

            model.IsFullLife = model.MissingHP > 0 ? false : true;

            if (model.IsFullLife)
            {
                return RedirectToAction("Index");
            }

            var userCoins = _db.Materials.Where(x => x.UserId == user.Id && x.Name == "Coins").FirstOrDefault();

            if (userCoins.Count < model.CostToHeal)
            {
                return RedirectToAction("Index");
            }

            user.Health = 100;
            userCoins.Count -= model.CostToHeal;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}