using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MiniSwitch.Enums;

namespace MiniSwitch.Models
{
    public class SourceNode
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
        [Required]
        public Scheme Scheme { get; set; }

        public NodeStatusEnum Status { get; set; }
    }
}
