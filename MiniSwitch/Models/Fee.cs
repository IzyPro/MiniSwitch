using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniSwitch.Enums;

namespace MiniSwitch.Models
{
    public class Fee
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public FeeTypeEnum FeeType { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Maximum { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Minimum { get; set; }
    }
}
