using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MiniSwitch.Enums;
using MiniSwitch.Models;

namespace MiniSwitch.ViewModels
{
    public class SourceNodeViewModel
    {
        //[Required]
        //public string Name { get; set; }
        //[Required]
        //public string HostName { get; set; }
        //[Required]
        //public string IpAddress { get; set; }
        //[Required]
        //public string Port { get; set; }
        //[Required]
        //public Scheme Scheme { get; set; }

        //public NodeStatusEnum Status { get; set; }
        public PaginatedList<SourceNode> SourceNodes { get; set; }
        public List<Scheme> Scheme { get; set; }
    }
}
