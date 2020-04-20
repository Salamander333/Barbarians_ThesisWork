using Barbarians.Data.GlobalEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbarians.Areas.Administration.Controllers
{
    public class AdministrationController : Controller
    {
        [Authorize(Roles = IdentityRoles.AdministratorRoleName)]
        [Area("Administration")]
        public IActionResult Index()
        {
            return View();
        }
    }
}