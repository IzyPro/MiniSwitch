using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MiniSwitch.Enums;
using MiniSwitch.Models;

namespace MiniSwitch.ViewModels
{
    public class SourceNodeViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HostName { get; set; }
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public string Port { get; set; }
        [Required]
        public Guid SchemeID { get; set; }
        public Scheme Scheme { get; set; }

        public NodeStatusEnum Status { get; set; }
    }
}
