using Barbarians.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Barbarians.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> user;

        public UsersController(UserManager<ApplicationUser> user)
        {
            this.user = user;
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return this.View();
        }

        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            return this.View();
        }
    }
}
