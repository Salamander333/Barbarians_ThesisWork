using Barbarians.Data.GlobalEnums;
using System.ComponentModel.DataAnnotations;

namespace Barbarians.Areas.Administration.ViewModels.Admin
{
    public class AdminAddArmorViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name is too short.")]
        [MaxLength(25, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Required]
        public ArmorTypes Type { get; set; }

        [Required]
        public Materials MaterialRequired { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Invalid material count. Must be more than 0 and less than 100.")]
        public int MaterialCount { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Invalid defence. Must be more than 0 and less than 50.")]
        public int Defence { get; set; }

        [Required]
        public int Cost { get; set; }
    }
}
