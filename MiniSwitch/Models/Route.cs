using System;
using System.ComponentModel.DataAnnotations;

namespace MiniSwitch.Models
{
    public class Route
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public SinkNode SinkNode { get; set; }

        public string CardPAN { get; set; }
        public string Description { get; set; }
    }
}
