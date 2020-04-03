using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbarians.Controllers
{
    public class BlacksmithController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index(string craft)
        {
            var partialName = "";

            if (craft == "Chest")
            {
                partialName = "_BlacksmithChest";
            }
            else if (craft == "Leggings")
            {
                partialName = "_BlacksmithLeggings";
            }
            else if (craft == "Boots")
            {
                partialName = "_BlacksmithBoots";
            }
            else if (craft == "Weapons")
            {
                partialName = "_BlacksmithWeapons";
            }
            else
            {
                return this.BadRequest("Not found");
            }


            return this.View((object)partialName);
        }
    }
}