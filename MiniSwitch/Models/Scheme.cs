using System;
using System.ComponentModel.DataAnnotations;
using MiniSwitch.Enums;

namespace MiniSwitch.Models
{
    public class Scheme
    {
        public Guid Id { get; set; }

        [Required]
        public Route Route { get; set; }
        [Required]
        public TransactionType TransactionType { get; set; }
        [Required]
        public Channel Channel { get; set; }
        [Required]
        public Fee Fee { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
