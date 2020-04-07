using Barbarians.Data.GlobalEnums;
using Barbarians.Models;

namespace Barbarians.ViewModels.Craftables
{
    public class CraftableArmorViewModel
    {
        public string Name { get; set; }

        public ArmorTypes Type { get; set; }

        public Materials MaterialRequired { get; set; }

        public int MaterialCount { get; set; }

        public int Defence { get; set; }

        public int CraftCost { get; set; }

        public ApplicationUser User { get; set; }
    }
}
