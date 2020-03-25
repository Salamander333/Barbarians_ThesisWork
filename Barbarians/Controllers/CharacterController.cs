using Barbarians.Data;
using Barbarians.Models;
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

        public CharacterController(SignInManager<ApplicationUser> manager,
            UserManager<ApplicationUser> user,
            ApplicationDbContext db)
        {
            this._manager = manager;
            this._user = user;
            this._db = db;
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
        public IActionResult Gather()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Gather(string type)
        {
            return null;
        }
    }
}
