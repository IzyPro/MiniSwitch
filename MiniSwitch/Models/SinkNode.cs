using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MiniSwitch.Enums;

namespace MiniSwitch.Models
{
    public class SinkNode
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string HostName { get; set; }
        [Required]
        public IPAddress IpAddress { get; set; }
        [Required]
        public string Port { get; set; }

        public NodeStatusEnum Status { get; set; }
    }
}
