using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Barbarians.Models
{
    public class Statue
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public string Stat { get; set; }

        public ICollection<UserStatue> UserStatues { get; }
    }
}
