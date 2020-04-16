using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Barbarians.Services;
using Barbarians.ViewModels.Arena;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Barbarians.Controllers
{
    public class ArenaController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IArenaService _arenaService;

        public ArenaController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IArenaService arenaService)
        {
            this._db = db;
            this._userManager = userManager;
            this._arenaService = arenaService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var contestants = _db.ApplicationUsers
                .Where(x => x.Id != _userManager.GetUserId(this.User))
                .Select(x => new ArenaContestantViewModel
                {
                    Name = x.UserName,
                    Defence =
                    (x.Armors.Where(s => s.UserId == x.Id && s.Type == ArmorTypes.Chest && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : x.Armors.Where(s => s.UserId == x.Id && s.Type == ArmorTypes.Chest).OrderByDescending(x => x.Defence).FirstOrDefault().Defence) +
                    (x.Armors.Where(s => s.UserId == x.Id && s.Type == ArmorTypes.Leggings && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : x.Armors.Where(s => s.UserId == x.Id && s.Type == ArmorTypes.Leggings).OrderByDescending(x => x.Defence).FirstOrDefault().Defence) +
                    (x.Armors.Where(s => s.UserId == x.Id && s.Type == ArmorTypes.Boots && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : x.Armors.Where(s => s.UserId == x.Id && s.Type == ArmorTypes.Boots).OrderByDescending(x => x.Defence).FirstOrDefault().Defence),
                    Damage = x.Weapons.Where(s => s.UserId == x.Id && s.IsBroken == false).OrderByDescending(x => x.Damage).First().Damage,
                    Coins = x.Materials.Where(s => s.UserId == x.Id).Where(x => x.Type == MaterialType.Currency).First().Count,
                    Health = x.Health
                })
                .ToList();

            return View(contestants);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Attack(string opponentName)
        {
            var attackerName = _userManager.GetUserName(this.User);
            var result = await _arenaService.AttackOpponent(attackerName, opponentName);

            return RedirectToAction("BattleResult", new { id = result});
        }

        [Authorize]
        [HttpGet]
        public IActionResult BattleResult(string id)
        {
            var report = _db.BattleReports.Where(x => x.Id == id).FirstOrDefault();
            if (_userManager.GetUserId(this.User) == report.AttackerId || _userManager.GetUserId(this.User) == report.OpponentId)
            {
                return this.View(new BattleReportViewModel { BattleLog = report.ReportString.Split(new[] { '\r', '\n' })});
            }

            return Json("Access denied.");
        }
    }
}