using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Barbarians.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Armors = new HashSet<Armor>();
            this.Weapons = new HashSet<Weapon>();
        }

        public ICollection<Armor> Armors { get; set; }

        public ICollection<Weapon> Weapons { get; set; }

        public ICollection<UserStatue> UserStatues { get; }
    }
}
