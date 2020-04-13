using Barbarians.Models;
using System.Collections.Generic;

namespace Barbarians.ViewModels.Character
{
    public class CharacterViewModel
    {
        public CharacterViewModel()
        {
            this.Armors = new HashSet<Armor>();
            this.Weapons = new HashSet<Weapon>();
            this.Statues = new HashSet<Statue>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public int Health { get; set; }

        public ICollection<Armor> Armors { get; set; }

        public ICollection<Weapon> Weapons { get; set; }

        public ICollection<Statue> Statues { get; set; }

        public ICollection<Material> Materials { get; set; }
    }
}
