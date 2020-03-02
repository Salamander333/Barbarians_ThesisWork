using Barbarians.Models;
using Barbarians.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Barbarians.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly SignInManager<ApplicationUser> _manager;

        public UsersController(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> manager)
        {
            this._user = user;
            this._manager = manager;
        }

        public IActionResult Login()
        {
            if (_manager.IsSignedIn(this.User))
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (_manager.IsSignedIn(this.User))
            {
                return this.Redirect("/");
            }

            return null;
        }

        public IActionResult Register()
        {
            if (_manager.IsSignedIn(this.User))
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (_manager.IsSignedIn(this.User))
            {
                return this.Redirect("/");
            }

            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                };

                var result = await _user.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _manager.SignInAsync(user, false);
                    return this.Redirect("/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return this.View();
        }
    }
}
