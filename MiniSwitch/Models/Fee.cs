using System;
using System.ComponentModel.DataAnnotations;
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

        public decimal Maximum { get; set; }
        public decimal Minimum { get; set; }
    }
}
