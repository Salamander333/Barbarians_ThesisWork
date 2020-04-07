using Barbarians.Models;
using System.Collections.Generic;

namespace Barbarians.ViewModels.Craftables
{
    public class CraftableModelForPartial
    {
        public List<Material> UserMaterials { get; set; }

        public List<CraftableArmorViewModel> CraftableArmors { get; set; }

        public List<CraftableWeaponViewModel> CraftableWeapons { get; set; }
    }
}
