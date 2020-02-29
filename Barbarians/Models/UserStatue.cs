using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbarians.Models
{
    public class UserStatue
    {
        [Key]
        [ForeignKey(nameof(Statue))]
        public string StatueId { get; set; }

        public Statue Statue { get; set; }

        [Key]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
