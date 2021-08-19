using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniSwitch.Models;

namespace MiniSwitch.ViewModels
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Fee { get; set; }
        [Required]
        public Guid SourceNodeID { get; set; }

        public SourceNode SourceNode { get; set; }
        //[Required]
        //public Guid SchemeID { get; set; }

        //public Scheme Scheme { get; set; }
    }
}
