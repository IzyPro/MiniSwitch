using System;
using System.ComponentModel.DataAnnotations;
using MiniSwitch.Models;

namespace MiniSwitch.ViewModels
{
    public class SchemeViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid RouteID { get; set; }

        public Route Route { get; set; }

        [Required]
        public Guid TransactionTypeID { get; set; }

        public TransactionType TransactionType { get; set; }
        [Required]
        public Guid ChannelID { get; set; }

        public Channel Channel { get; set; }
        [Required]
        public Guid FeeID { get; set; }

        public Fee Fee { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
