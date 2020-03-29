using Barbarians.Data.GlobalEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbarians.Models
{
    public class TaskGather
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        [Required]
        public Materials Rescource { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int GoldIncome { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
