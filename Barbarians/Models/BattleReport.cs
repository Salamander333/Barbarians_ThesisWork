using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbarians.Models
{
    public class BattleReport
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ReportString { get; set; }

        [Required]
        public string AttackerId { get; set; }

        [Required]
        public string OpponentId { get; set; }
    }
}
