using Barbarians.Data;
using Barbarians.Models;
using Barbarians.ViewModels.Logs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Barbarians.Controllers
{
    public class LogsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public LogsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var logs = _db.BattleReports
                .Where(x => x.AttackerId == user.Id || x.OpponentId == user.Id)
                .Select(x => new BattleLogsViewModel
                {
                    Id = x.Id,
                    AttackerName = _db.Users.FirstOrDefault(u => u.Id == x.AttackerId).UserName,
                    DefendantName = _db.Users.FirstOrDefault(u => u.Id == x.OpponentId).UserName,
                    Date = x.Date
                })
                .OrderByDescending(x => x.Date)
                .ToList();

            return View(logs);
        }
    }
}