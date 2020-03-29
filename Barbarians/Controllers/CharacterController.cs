using Barbarians.Data;
using Barbarians.Models;
using Barbarians.Services;
using Barbarians.ViewModels.Character;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Barbarians.Controllers
{
    public class CharacterController : Controller
    {
        private readonly SignInManager<ApplicationUser> _manager;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ApplicationDbContext _db;
        private readonly ITasksService _tasksService;

        public CharacterController(SignInManager<ApplicationUser> manager,
            UserManager<ApplicationUser> user,
            ApplicationDbContext db,
            ITasksService tasksService)
        {
            this._manager = manager;
            this._user = user;
            this._db = db;
            this._tasksService = tasksService;
        }

        public IActionResult Index()
        {
            if (_manager.IsSignedIn(this.User))
            {
                var model = new CharacterViewModel
                {
                    Username = this.User.Identity.Name,
                    Materials = _db.Materials.Where(x => x.UserId == _user.GetUserId(this.User)).ToList(),
                };

                return this.View(model);
            }

            return this.Redirect("/");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Gather()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Gather(int id)
        {
            var material = "";
            var difficulty = "";

            var command = Request.Form.First().Key.Split(":");
            if (command.Count() == 2)
            {
                material = command[0];
                difficulty = command[1];
            }

            var validEntry = _tasksService.IsGatheringTaskValid(material, difficulty);
            if (validEntry)
            {
                return Json("succ");
            }
            else
            {
                return this.Redirect("Gather");
            }
        }
    }
}
