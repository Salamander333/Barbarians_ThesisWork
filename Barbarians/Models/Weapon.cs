﻿using Barbarians.Data.GlobalEnums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbarians.Models
{
    public class Weapon
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public WeaponTypes Type { get; set; }

        [Required]
        public int Damage { get; set; }

        [Required]
        public bool IsBroken { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
