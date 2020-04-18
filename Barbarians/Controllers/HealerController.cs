using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbarians.Controllers
{
    public class HealerController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}