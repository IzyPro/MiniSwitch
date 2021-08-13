using System;
using System.ComponentModel.DataAnnotations;

namespace MiniSwitch.Models
{
    public class Channel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Fee Fee { get; set; }
    }
}
