using System;
using System.ComponentModel.DataAnnotations;
using MiniSwitch.Models;

namespace MiniSwitch.ViewModels
{
    public class RouteViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public Guid SinkNodeID { get; set; }

        public SinkNode SinkNode { get; set; }

        public string BIN { get; set; }
        public string Description { get; set; }
    }
}
