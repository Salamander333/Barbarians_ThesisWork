using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Barbarians.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Barbarians.Controllers
{
    public class BlacksmithController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _user;
        private readonly IBlacksmithService _blacksmithService;
        private readonly ITasksService _tasksService;

        public BlacksmithController(ApplicationDbContext db,
            UserManager<ApplicationUser> user,
            IBlacksmithService blacksmithService,
            ITasksService tasksService)
        {
            this._db = db;
            this._user = user;
            this._blacksmithService = blacksmithService;
            this._tasksService = tasksService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string craft)
        {
            var userId = await _user.GetUserAsync(this.User);

            await _tasksService.CheckGatheringTaskCompletion(userId.Id);

            if (!Enum.IsDefined(typeof(ArmorTypes), craft) && craft != "Weapons")
            {
                return this.BadRequest("Page not found.");
            }

            var model = _blacksmithService.CreateCraftableModel(craft, _user.GetUserId(this.User));

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CraftArmor(string id)
        {
            var user = await _user.GetUserAsync(this.User);
            await _blacksmithService.AddArmorItemToUser(id, user.Id);

            return this.Redirect("/Character");
        }
    }
}