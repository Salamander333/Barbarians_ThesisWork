using Barbarians.Data.GlobalEnums;
using Barbarians.Models;

namespace Barbarians.ViewModels.Craftables
{
    public class CraftableWeaponViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public WeaponTypes Type { get; set; }

        public Materials MaterialRequired { get; set; }

        public int MaterialCount { get; set; }

        public int Damage { get; set; }

        public int CraftCost { get; set; }

        public ApplicationUser User { get; set; }
    }
}
