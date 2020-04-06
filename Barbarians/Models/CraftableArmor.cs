using Barbarians.Data.GlobalEnums;
using System.ComponentModel.DataAnnotations;

namespace Barbarians.Models
{
    public class CraftableArmor
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public ArmorTypes Type { get; set; }

        [Required]
        public Materials MaterialRequired { get; set; }

        [Required]
        public int MaterialCount { get; set; }

        [Required]
        public int Defence { get; set; }

        [Required]
        public int CraftCost { get; set; }
    }
}
