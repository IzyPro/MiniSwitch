using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSwitch.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Fee { get; set; }
        [Required]
        public SourceNode SourceNode { get; set; }
        //[Required]
        //public Scheme Scheme { get; set; }
    }
}
